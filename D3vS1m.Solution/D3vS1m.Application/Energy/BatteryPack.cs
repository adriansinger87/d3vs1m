using D3vS1m.Application.Devices;
using D3vS1m.Domain.System.Enumerations;
using System;

namespace D3vS1m.Application.Energy
{
    [Serializable]
    public class BatteryPack : PartBase
    {
        // --- fields

        // --- constructor

        #region constructor (2)
        /// <summary>
        /// Constructs a new instance with standard properites
        /// </summary>
        public BatteryPack()
        {
            Name = Models.BatteryPack;
            Type = PartTypes.PowerSupply;

            State = new BatteryState();
            State.Init(this);
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

        // --- properties

        public BatteryState State { get; set; }

        public string DischargeFunction
        {
            get
            {
                string s = "y = ";

                // vorwärts-Schleife
                for (int i = 0; i < this.Polynom.Length; i++)
                {
                    s += "(" + this.Polynom[i] + ")";

                    if (i > 0)
                    {
                        s += " x^" + i;
                    }

                    if (i < this.Polynom.Length - 1)
                    {
                        s += " + ";
                    }
                }

                return s;
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
    }
}
