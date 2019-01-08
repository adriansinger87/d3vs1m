using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.System.Constants
{
    public partial class Const
    {
        /// <summary>
        /// Defines the constants for all communication channels i.e. the nested class Radio with its constants. 
        /// </summary>
        public class Channel
        {
            public class Radio
            {
                public class Space
                {
                    public static readonly Vertex MinCorner = new Vertex(-5, -5, -5);
                    public static readonly Vertex MaxCorner = new Vertex(5, 5, 5);
                    public const float Resolution = 1;
                }

                public const float AttenuationExponent = 1.25F;
                public const float AttenuationOffset = 0;

                public const float EPSILON = 0.000001F;
                public const int LIGHTSPEED = 299792458;                // [m/s]

                public const float U0 = (float)(1.25663706144e-6);      // mag. Feldkonstante Permeabilit#t u0 [Vs/Am]
                public const float E0 = (float)(8.85418781762e-12);     // el. Feldkonstante Permittivität e0  [As/Vm]
                public const float Z0 = 376.73f;                        // Wellenwiderstand in Ohm für Freiraum

                // -- methods

                public static float FreqToMeter(float mhz)
                {
                    return LIGHTSPEED / (mhz * 1000000);
                }
            }
        }
    }
}
