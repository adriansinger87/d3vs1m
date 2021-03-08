using D3vS1m.Application.Devices;
using D3vS1m.Domain.System.Enumerations;
using System;
using System.Text;

namespace D3vS1m.Application.Energy
{
    [Serializable]
    public class BatteryPack : PartBase
    {
        // --- fields

        // --- properties

        public BatteryState State { get; set; }

        public string DischargeFunction
        {
            get
            {
                var s = new StringBuilder("y = ");

                for (int i = 0; i < this.Polynom.Length; i++)
                {
                    s.Append($"({this.Polynom[i]})");

                    if (i > 0)
                    {
                        s.Append($" x^ {i}");
                    }

                    if (i < this.Polynom.Length - 1)
                    {
                        s.Append(" + ");
                    }
                }

                return s.ToString();
            }
        }

        /// <summary>
        /// Gibt an oder legt fest, welches Entladeverhalten zur Berechnung des State-of-Discharge Wertes dient.
        /// Das Polynom sollte nur als Array gesetzt werden und nicht dessen Einzelelemente.
        /// </summary>
        public float[] Polynom { get; set; }

        /// <summary>
        /// Gibt an oder legt fest, ob die Batterie erschöpflich ist oder nicht.
        /// </summary>
        public bool IsUnlimited { get; set; }

        /// <summary>
        /// Gibt an, oder legt fest, ob es sich bei dem Energieträger um eine wieder aufladbare Quelle handelt oder nicht.
        /// </summary>
        public bool IsRechargeable { get; set; }

        /// <summary>
        /// Gibt die Abschaltspannung aus, ab der das Objekt ein Gerät nicht länger mit Energie versorgen kann.
        /// </summary>
        public float CutoffVoltage { get; set; }

        /// <summary>
        /// Gibt an oder legt fest, wie viel Strom maximal entnommen werden kann.
        /// </summary>
        public float MaxDischargeCurrent { get; set; }

        /// <summary>
        /// Gibt an oder legt fest, wie viele Zellen im Batteriepack enthalten sind.
        /// Beim setzen des Wertes müssen Eigenchaften wie Spannung oder Ladung neu berechnet werden.
        /// </summary>
        public int NumberOfCells { get; set; }

        /// <summary>
        /// Gibt an oder legt fest, ob die Zellen parallel (true) oder in Reihe (false) geschalten sind.
        /// Beim setzen des Wertes müssen Eigenchaften wie Spannung oder Ladung neu berechnet werden.
        /// </summary>
        public bool ParallelCellPack { get; set; }

        // --- constructor

        #region constructor (2)
        /// <summary>
        /// Constructs a new instance with standard properites
        /// </summary>
        public BatteryPack()
        {
            Name = Models.Energy.Battery.Name;
            Type = PartTypes.PowerSupply;
            State = BatteryState.R6;
            Polynom = BatteryState.R6_POLYNOM;
        }

        /// <summary>
        /// Constructs a new instance with a specific name
        /// </summary>
        /// <param name="name"></param>
        public BatteryPack(string name) : this()
        {
            base.Name = name;
        }
        #endregion

        // --- public methods

        public override string ToString()
        {
            return base.Name + " " + this.DischargeFunction;
        }

       
    }
}
