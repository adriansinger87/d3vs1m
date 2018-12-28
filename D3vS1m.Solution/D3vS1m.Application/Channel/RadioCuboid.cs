using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Logging;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Channel
{
    /// <summary>
    /// Stores the data of the cuboid that is used as input for the wireless radio channel simulation
    /// </summary>
    public class RadioCuboid
    {
        public RadioCuboid()
        {
            this.MinCorner = new Vector(-5, -5, -5);
            this.MaxCorner = new Vector(5, 5, 5);
            this.Resolution = 1;
        }

        // --- public methoden

        public HashSet<float[]> CreateRxPositions()
        {
            if (DataDimension == null)
            {
                DataDimension = new int[3] {
                     (int)Math.Ceiling((MaxCorner.X - MinCorner.X) / (Resolution)),
                     (int)Math.Ceiling((MaxCorner.Y - MinCorner.Y) / (Resolution)),
                     (int)Math.Ceiling((MaxCorner.Z - MinCorner.Z) / (Resolution))
                 };
                TotalData = DataDimension[0] * DataDimension[1] * DataDimension[2];
            }

            HashSet<float[]> positions = new HashSet<float[]>();
            float mid = Resolution / 2;
            // 'mid' pushes the data-point into the middle of an imaginary cube with the length of box.Resolution
            // prevent the point to reach the edges of box
            for (float x = MinCorner.X + mid; x < MaxCorner.X; x = (float)Math.Round(x + Resolution, 3))
            {
                for (float y = MinCorner.Y + mid; y < MaxCorner.Y; y = (float)Math.Round(y + Resolution, 3))
                {
                    for (float z = MinCorner.Z + mid; z < MaxCorner.Z; z = (float)Math.Round(z + Resolution, 3))
                    {
                        positions.Add(new float[] { x, y, z });
                    }
                }
            }

            Log.Info($"{positions.Count} rx positions created");

            return positions;
        }

        public override string ToString()
        {
            return $"Min: {MinCorner} Max: {MaxCorner}";
        }

        // --- properties

        public Vector MinCorner { get; set; }
        public Vector MaxCorner { get; set; }
        public float Resolution { get; set; }
        public int[] DataDimension { get; set; }
        public int TotalData { get; set; }
    }
}
