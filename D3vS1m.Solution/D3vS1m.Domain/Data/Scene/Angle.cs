using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Data.Scene
{
    public class Angle
    {
        public Angle()
        {
            // TODO: remove magic numbers
            Azimuth = 0;
            Elevation = 90;
        }

        public Angle(float azimuth, float elevation)
        {
            this.Azimuth = azimuth;
            this.Elevation = elevation;
        }

        public override string ToString()
        {
            return $"Az: {Azimuth}, El: {Elevation}";
        }

        /// <summary>
        /// The east-west angle on a sphere
        /// In antenna characteristics it is the horizontal angle
        /// </summary>
        public float Azimuth { get; set; }

        /// <summary>
        /// The north-south angle on a sphere
        /// In antenna characteristics it is the vertical angle
        /// </summary>
        public float Elevation { get; set; }
    }
}
