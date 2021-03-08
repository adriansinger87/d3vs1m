using System;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Microsoft.Extensions.Logging;
using TeleScope.Logging;
using static D3vS1m.Application.Energy.BatteryState;

namespace D3vS1m.Application.Energy
{
	[Serializable]
	public class BatteryPackSimulator : SimulatorBase
	{
		// -- fields

		private readonly ILogger<BatteryPackSimulator> _log;

		private BatteryArgs _batteryArgs;
		private NetworkArgs _networkArgs;
		private WirelessCommArgs _commArgs;
		private RuntimeArgs _runArgs;

		// -- properties
		public override string Key => Models.Energy.Battery.Name;
		public override string Name => Models.Energy.Battery.Name;
		public override SimulationTypes Type => SimulationTypes.Energy;
		public override ArgumentsBase Arguments => _batteryArgs;

		// -- connstructor 

		/// <summary>
		/// Baware: no runtime will be usable
		/// </summary>
		public BatteryPackSimulator() : this(null)
		{
		}

		public BatteryPackSimulator(RuntimeBase runtime) : base(runtime)
		{
			_log = LoggingProvider.CreateLogger<BatteryPackSimulator>();
		}

		// -- methods

		public override ISimulatable With(ArgumentsBase arguments)
		{
			if (ConvertArgs(arguments, ref _batteryArgs)) return this;
			if (ConvertArgs(arguments, ref _networkArgs)) return this;
			if (ConvertArgs(arguments, ref _commArgs)) return this;
			if (ConvertArgs(arguments, ref _runArgs)) return this;
			else return ArgsNotAdded(arguments.Name);
		}

		public override void Run()
		{
			base.BeforeExecution();

			// TODO the discharge current should come from the energy consumption based on the device / comm. simulation 
			var current = 100;

			_networkArgs.Network.Items.ForEach(d =>
			{
				var powerSupply = d.Parts.GetPowerSupply();
				if (powerSupply != null)
				{
					var battery = powerSupply as BatteryPack;
					Discharge(battery, current, _runArgs.CycleDuration);
				}
			});

			base.AfterExecution();
		}

		public void Discharge(BatteryPack battery, float current, TimeSpan time)
		{
			if (!Check(battery))
			{
				return;
			}

			// Vorbereitungen...
			var seconds = (float)time.TotalSeconds;
			var initial = battery.State.Initial;
			var now = battery.State.Now;

			// Berechnungen...
			now.TemperaturFactor = 1;
			now.CurrentFactor = 1;
			SetSelfDischarge(initial, now, seconds);
			float qt = GetChargeConsumption(now, seconds, current);

			SetStateOfDischarge(initial, now, qt);

			now.Voltage = GetDischargeVoltage(battery, now);
			now.Charge += qt;
			now.AddTime(time);

			// finish & check again
			Check(battery);
		}

		private float GetChargeConsumption(BatteryFields state, float seconds, float current)
		{
			/*
             * Berechnung des Ladungsverbrauchs für den Zeitraum t:
             * -----------------------------------------------------
             * qt = (i * t * at * bt) + (at * gt)
             * 
             * qt		-> verbrauchte Ladung für den Zeitraum t
             * i		-> Strom ("mA")
             * t        -> Zeitraum in h, damit sich mAh ergeben
             * at		-> Korrekturfaktor alpha_t	(Temperaturkorrekturfaktor)
             * bt		-> Korrekturfaktor beta_t	(Stromkorrekturfaktor)
             * gt		-> Korrekturfaktor gamma_t	(Selbstentladungsfaktor)
             *
             * qn = Summe aller qt über die Zeit, quasi die gesamte verbrauchte Ladung
             */
			return (current * (seconds / 3600) * state.TemperaturFactor * state.CurrentFactor) + (state.TemperaturFactor * state.SelfDischarge);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="state"></param>
		/// <param name="qt">Charge of a time span t</param>
		/// <returns></returns>
		private void SetStateOfDischarge(BatteryFields initial, BatteryFields now, float qt)
		{
			/*
            * Berechnung des neuen State-Of-Discharge mittels des neuen timeSlots
            * SOD = SOD_0 + ((1 - SOD_0) / Q_0) * (qn) <--- PRÜFEN
            *
            * SOD      -> aktuell berechneter State-of-Discharge
            * SOD_0	-> initiale Selbstentladung "init_sod"
            * Q_0		-> initiale el. Ladung des BatteriePacks "Initial.Charge"
            */
			now.SoD = initial.SoD + ((1 - initial.SoD) / initial.Charge) * (qt + now.Charge);
		}

		/// <summary>
		/// Berechnet den für den Zeitschlitz zutreffenden Anteil der Selbstentladung des Energieträgers
		/// </summary>
		/// <param name="state">Zustandseigenschaften der Batterie</param>
		/// <param name="seconds">Zeitschlitz in Sekunden</param>
		private void SetSelfDischarge(BatteryFields initial, BatteryFields now, float seconds)
		{
			// Umrechnung sdr pro Jahr in sdr pro Sekunde: [ / Tag * h * min * s] [ / 365 * 24 * 60 * 60]; -> "31536000"
			var availableCharge = initial.Charge - now.Charge;
			var selfDischarge = availableCharge * now.SDR;
			selfDischarge /= 31536000;
			selfDischarge *= seconds;

			now.SelfDischarge = selfDischarge;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>the resulting voltage </returns>
		private float GetDischargeVoltage(BatteryPack battery, BatteryFields now)
		{
			/*
             * Berechnung des Entladeverhaltens:
             * ---------------------------------
             * Polynom-Funktion des n ten-Grades, bestimmt durch die Länge des Arrays "polynomials"
             * die Laufvariable i entspricht der Potenz:
             * [0] -> x^0 | [1] -> x^1 | [2] -> x^2 ...
             *
             * resultierende Formel:
             * --------------------
             * v = poly.[0] + poly.[1] * sod ^ 1 + poly.[2] * sod ^ [2] ...
             * v *= Temperaturkorrektur
             */
			float v = 0;
			for (int i = 0; i < battery.Polynom.Length; i++)
			{
				v += (float)(battery.Polynom[i] * Math.Pow(now.SoD, i));
			}
			v *= now.TemperaturFactor;

			return v;
		}

		/// <summary>
		/// Check if the battery is out of energy or not
		/// </summary>
		/// <param name="battery"></param>
		/// <returns></returns>
		private bool Check(BatteryPack battery)
		{
			if (battery.IsUnlimited)
			{
				return true;
			}
			else
			{
				return !battery.State.IsDepleted;
			}
		}


	}
}
