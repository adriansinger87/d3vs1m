using System;

namespace D3vS1m.Application.Energy
{
    [Serializable]
    public class BatteryState
    {
        // -- fields

        private StateFields _now;

        // -- properties

        public StateFields Initial { get; set; }
        public StateFields Now
        {
            get { return _now; }
            set { _now = value; }
        }

        private float _cutoffVoltage;

        public BatteryState()
        {

        }

        public void Init(BatteryPack battery)
        {
            // default Battery R6 
            var polynom = new float[6] {
                1.385749902f,
                -1.147810559f,
                6.134051602f,
                -16.1755085f,
                19.33509917f,
                -8.52820309f
            };
            StateFields fields = new StateFields
            {
                Voltage = polynom[0],       // initial voltage is the x^0 in the polynom
                SoD = 0,                    // 100% loaded
                SDR = 0.15f,                // 15% p.a.
                Charge = 2700               // mAh
            };

            Init(battery, fields, polynom, 0.9f, 200f);
        }

        public void Init(BatteryPack battery, StateFields fields, float[] polynom, float cutoffVoltage, float maxDischargeCurrent)
        {
            battery.Polynom = polynom;
            battery.CutoffVoltage = cutoffVoltage;
            _cutoffVoltage = cutoffVoltage;
            battery.MaxDischargeCurrent = maxDischargeCurrent;

            // copy properties
            Initial = fields;
            _now = fields;
            _now.Charge = 0;    // actual used load should be zero and will be increased during simulation
        }



        public override string ToString()
        {
            return $"Initial: {Initial.ToString()}, Now: {Now.ToString()}";
        }

        // -- properties

        /// <summary>
        /// Gets the information wheather the battery pack is empty or not. 
        /// </summary>
        public bool IsDepleted
        {
            get { return (Now.SoD >= 1 || Now.Voltage <= _cutoffVoltage); }
        }

        // -- inner struct

        public struct StateFields
        {
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
