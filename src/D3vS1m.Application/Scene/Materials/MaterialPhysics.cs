using D3vS1m.Domain.System.Constants;
using System;

namespace D3vS1m.Application.Scene.Materials
{
    /// <summary>
    /// Describes some physical properties of a certain material instance for radio simulation.
    /// </summary>
    [Serializable]
    public class MaterialPhysics
    {
        public MaterialPhysics()
        {
            Frequency = Const.Scene.Frequency;
            PenetrationLoss = Const.Scene.PenetrationLoss;
            RelativePermeability = Const.Scene.RelativePermeability;
            RelativePermittivity = Const.Scene.RelativePermittivity;

            CalcReflectionValues();
        }

        /// <summary>
        /// Calculates the reflexion factor and reflection loss based on its input properties and
        /// stores the values in the regarding properites.
        /// </summary>
        public void CalcReflectionValues()
        {
            /*
             * Z0 -> ~377 ohm
             *			    ____
             *			   / ur |
             * Zm =	Z0 *  /	---
             *			 V	 er
             *			 
             *	   |  Zm - Z0  |
             * R = | --------- |
             *	   |  Zm + Z0  |
             */
            float z0 = Const.Channel.Radio.Z0;
            float zm = (float)(z0 * Math.Sqrt(RelativePermeability / RelativePermittivity));
            ReflectionFactor = Math.Abs((zm - z0) / (zm + z0));
            ReflectionLoss = (float)(-10 * Math.Log10(ReflectionFactor));
        }

        // --- methods

        public override string ToString()
        {
            return $"penetration loss {this.PenetrationLoss} @ {this.Frequency} MHz";
        }

        // -- properties

        /// <summary>
        /// Gets or sets the frequency in [MHz].
        /// </summary>
        public float Frequency { get; set; }

        /// <summary>
        /// Gets or sets the empirical estimated penetration loss in [dBm] of the material at the regarding frequency.
        /// </summary>
        public float PenetrationLoss { get; set; }

        /// <summary>
        /// Gets or sets the relative permeability 'ur' in [Vs/Am] of the material at the regarding frequency .
        /// </summary>
        public float RelativePermeability { get; set; }

        /// <summary>
        /// Gets or sets the relative permittivity 'er' in [As/Vm] of the material at the regarding frequency.
        /// </summary>
        public float RelativePermittivity { get; set; }

        /// <summary>
        /// Gets the reflection factor in [0..1] of the material at the regarding frequency.
        /// </summary>
        public float ReflectionFactor { get; private set; }

        /// <summary>
        /// Gets the reflexion loss in [dBm] of the material at the regarding frequency.
        /// </summary>
        public float ReflectionLoss { get; private set; }
    }
}
