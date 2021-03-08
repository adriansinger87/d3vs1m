using System;

namespace D3vS1m.Application.Energy
{
	[Serializable]
	public class BatteryState
	{
		// -- static fields

		private static readonly float[] DEFAULT_POLYNOM = {
			1.385749902f,
			-1.147810559f,
			6.134051602f,
			-16.1755085f,
			19.33509917f,
			-8.52820309f
		};

		private static readonly BatteryFields R6_FIELDS = new BatteryFields
		{
			CutoffVoltage = 0.9F,
			SoD = 0,
			SDR = 0.15f,
			Charge = 2700,
			Voltage = DEFAULT_POLYNOM[0],
			TemperaturFactor = 1F,
			CurrentFactor = 1F,
			SelfDischarge = 0,
			ElapsedTime = new TimeSpan()
		};

		// -- instance fields

		private readonly BatteryFields _initial;
		private readonly BatteryFields _now;

		// --constructors

		public BatteryState() : this(R6_FIELDS)
		{	
		}

		public BatteryState(BatteryFields fields)
		{
			_initial = fields;
			_now = fields;
			_now.Charge = 0;    // actual used load should be zero and will be increased during simulation
		}

		public BatteryFields Initial()
		{
			return _initial;
		}

		public BatteryFields Now()
		{
			return _now;
		}

		public override string ToString()
		{
			return $"Initial: {_initial}, Now: {_now}";
		}

		// -- properties

		/// <summary>
		/// Gets the information wheather the battery pack is empty or not. 
		/// </summary>
		public bool IsDepleted
		{
			get { return (_now.SoD >= 1 || _now.Voltage <= _initial.CutoffVoltage); }
		}
		
		// -- inner struct

		public struct BatteryFields
		{

			public float CutoffVoltage { get; set; }

			/// <summary>
			/// Gibt den aktuellen Stand der Entladung (State-of-Discharge) [1..0] aus oder legt diesen fest.
			/// </summary>
			public float SoD { get; set; }

			/// <summary>
			/// Gibt die Selbstentladungsrate (Self-Discharge-Ratio) der Zelle aus die in einem Jahr "Nichtstun" entsteht oder legt diese fest.
			/// </summary>
			public float SDR { get; set; }

			/// <summary>
			/// Gibt aktuelle Spannung des Objektes aus.
			/// </summary>
			public float Voltage { get; set; }

			/// <summary>
			/// Gibt die verbrauchte elektrische Ladung aus oder legt diese fest.
			/// </summary>
			public float Charge { get; set; }

			/// <summary>
			/// Gibt die bereits abgelaufene Zeit in Sekunden aus oder legt diese fest.
			/// </summary>
			public TimeSpan ElapsedTime { get; set; }

			/// <summary>
			/// 
			/// </summary>
			public float TemperaturFactor { get; set; }

			/// <summary>
			/// 
			/// </summary>
			public float CurrentFactor { get; set; }

			/// <summary>
			/// Gibt die momentane Selbstentladung in mAh aus oder legt diese fest.
			/// </summary>
			public float SelfDischarge { get; set; }

			// -- methods

			/// <summary>
			/// Adds a new time span to the existing elapsed time object.
			/// </summary>
			/// <param name="ts">the new time span to add</param>
			public void AddTime(TimeSpan ts)
			{
				ElapsedTime = ElapsedTime.Add(ts);
			}

			public override string ToString()
			{
				return $"SoD: {SoD}, time: {ElapsedTime}";
			}
		}
	}
}
