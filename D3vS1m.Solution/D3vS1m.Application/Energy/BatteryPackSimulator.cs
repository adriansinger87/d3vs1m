using D3vS1m.Application.Runtime;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D3vS1m.Application.Energy
{
    public class BatteryPackSimulator : SimulatorBase
    {
        // -- fields

        private BatteryArgs _batteryArgs;
        private RuntimeArgs _runArgs;

        // -- connstructor

        /// <summary>
        /// Baware: no runtime will be usable
        /// </summary>
        public BatteryPackSimulator() : this(null)
        {
        }

        public BatteryPackSimulator(RuntimeBase runtime) : base(runtime)
        {
        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _batteryArgs))   return this;
            if (ConvertArgs(arguments, ref _runArgs))       return this;
            else                                            return ArgsNotAdded(arguments.Name);
        }

        public override void Run()
        {
            base.BeforeExecution();

            // TODO the discharge current should come from the energy consumption based on the device / comm. simulation 
            var current = 100;

            _batteryArgs.Batteries.ForEach(b => Discharge(b, current, _runArgs.CycleDuration));

            base.AfterExecution();
        }

        private void Discharge(BatteryPack battery, float current, TimeSpan time)
        {
            if (Check(battery) == false)
            {
                return;
            }

            // Vorbereitungen...
            var seconds = (float)time.TotalSeconds;
            var state = battery.State;

            // Berechnungen...
            state.Now.TemperaturFactor = 1;
            state.Now.CurrentFactor = 1;
            SetSelfDischarge(battery.State, seconds);
            float qt = GetChargeConsumption(state, seconds, current);

            SetStateOfDischarge(state, qt);

            state.Now.Voltage = GetDischargeVoltage(battery);
            state.Now.Charge += qt;
            state.Now.AddTime(time);

            // finish & check again
            Check(battery);
        }

        private float GetChargeConsumption(BatteryState state, float seconds, float current)
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
            return (float)(current * (seconds / 3600) * state.Now.TemperaturFactor * state.Now.CurrentFactor) + (state.Now.TemperaturFactor * state.Now.SelfDischarge);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="qt">Charge of a time span t</param>
        /// <returns></returns>
        private void SetStateOfDischarge(BatteryState state, float qt)
        {
            /*
            * Berechnung des neuen State-Of-Discharge mittels des neuen timeSlots
            * SOD = SOD_0 + ((1 - SOD_0) / Q_0) * (qn) <--- PRÜFEN
            *
            * SOD      -> aktuell berechneter State-of-Discharge
            * SOD_0	-> initiale Selbstentladung "init_sod"
            * Q_0		-> initiale el. Ladung des BatteriePacks "init_charge"
            */
            state.Now.SoD = state.Initial.SoD + ((1 - state.Initial.SoD) / state.Initial.Charge) * (qt + state.Now.Charge);
        }
        
        /// <summary>
        /// Berechnet den für den Zeitschlitz zutreffenden Anteil der Selbstentladung des Energieträgers
        /// </summary>
        /// <param name="state">Zustandseigenschaften der Batterie</param>
        /// <param name="seconds">Zeitschlitz in Sekunden</param>
        private void SetSelfDischarge(BatteryState state, float seconds)
        {
            // Umrechnung sdr pro Jahr in sdr pro Sekunde: [ / Tag * h * min * s] [ / 365 * 24 * 60 * 60]; -> "31536000"
            var availableCharge = state.Initial.Charge - state.Now.Charge;
            var selfDischarge = availableCharge * state.Now.SDR;
            selfDischarge /= 31536000;
            selfDischarge *= seconds;

            state.Now.SelfDischarge = selfDischarge;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>the resulting voltage </returns>
        private float GetDischargeVoltage(BatteryPack battery)
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
                v += (float)(battery.Polynom[i] * Math.Pow(battery.State.Now.SoD, i));
            }
            v *= battery.State.Now.TemperaturFactor;

            return v;
        }

        /// <summary>
        /// Check if the battery is out of energy or not
        /// </summary>
        /// <param name="battery"></param>
        /// <returns></returns>
        private bool Check(BatteryPack battery)
        {
            if (battery.IsUnlimited == true)
            {
                return true;
            }
            else if (battery.State.Now.Charge >= battery.State.Initial.Charge ||	// used charge
                     battery.State.Now.Voltage <= battery.CutoffVoltage)		    // too low voltage
            {
                //battery.State.Now.SoD = 1;
                return false;
            }
            else
            {
                return true;
            }
        }
        
        // -- properties

        public override string Name { get { return _batteryArgs.Name; } }

        public override SimulationModels Type { get { return SimulationModels.Energy; } }

        public override ArgumentsBase Arguments { get { return _batteryArgs; } }
    }
}
