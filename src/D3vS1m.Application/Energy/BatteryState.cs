using System;

namespace D3vS1m.Application.Energy
{
	[Serializable]
	public class BatteryState
	{
		// -- static fields

		public static readonly float[] R6_POLYNOM = {
			1.385749902f,
			-1.147810559f,
			6.134051602f,
			-16.1755085f,
			19.33509917f,
			-8.52820309f
		};

		public static readonly BatteryFields R6_FIELDS = new BatteryFields
		{
			CutoffVoltage = 0.9F,
			SoD = 0,
			SDR = 0.15f,
			Charge = 2700,
			Voltage = R6_POLYNOM[0],
			TemperaturFactor = 1F,
			CurrentFactor = 1F,
			SelfDischarge = 0,
			ElapsedTime = new TimeSpan()
		};

		// -- instance fields

		// -- properties

		public static BatteryState R6 => new BatteryState(R6_FIELDS);

		public BatteryFields Initial { get; private set; }

		public BatteryFields Now { get; private set; }

		/// <summary>
		/// Gets the information wheather the battery pack is empty or not. 
		/// </summary>
		public bool IsDepleted
		{
			get { return (Now.SoD >= 1 || Now.Voltage <= Initial.CutoffVoltage); }
		}

		// --constructors

		public BatteryState(BatteryFields fields)
		{
			Initial = fields;
			Now = fields.Copy();
			Now.Charge = 0;    // actual used load should be zero and will be increased during simulation
		}


		public override string ToString()
		{
			return $"Initial: {Initial}, Now: {Now}";
		}
		
		// -- inner struct

		public class BatteryFields
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

			public BatteryFields Copy()
			{
				return (BatteryFields) this.MemberwiseClone();
			}

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
