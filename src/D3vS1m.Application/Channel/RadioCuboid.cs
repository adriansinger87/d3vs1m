using D3vS1m.Domain.Data.Scene;
using Sin.Net.Domain.Logging;
using System;

namespace D3vS1m.Application.Channel
{
    /// <summary>
    /// Stores the data of the cuboid that is used as input for the wireless radio channel simulation
    /// </summary>
    [Serializable]
    public class RadioCuboid
    {

        public RadioCuboid()
        {
            MinCorner = Domain.System.Constants.Const.Channel.Radio.Space.MinCorner;
            MaxCorner = Domain.System.Constants.Const.Channel.Radio.Space.MaxCorner;
            Resolution = Domain.System.Constants.Const.Channel.Radio.Space.Resolution;
        }

        // --- public methoden

        public Vertex[] CreateRxPositions()
        {
            DataDimension = new int[3] {
                 (int)Math.Ceiling((MaxCorner.X - MinCorner.X) / (Resolution)),
                 (int)Math.Ceiling((MaxCorner.Y - MinCorner.Y) / (Resolution)),
                 (int)Math.Ceiling((MaxCorner.Z - MinCorner.Z) / (Resolution))
             };
            TotalData = DataDimension[0] * DataDimension[1] * DataDimension[2];

            Vertex[] positions = new Vertex[TotalData];
            float mid = Resolution / 2;
            // 'mid' pushes the data-point into the middle of an imaginary cube with half of the length of resolution
            // prevent the point to reach the edges of box
            int i = 0;
            for (float x = MinCorner.X + mid; x < MaxCorner.X; x = (float)Math.Round(x + Resolution, 3))
            {
                for (float y = MinCorner.Y + mid; y < MaxCorner.Y; y = (float)Math.Round(y + Resolution, 3))
                {
                    for (float z = MinCorner.Z + mid; z < MaxCorner.Z; z = (float)Math.Round(z + Resolution, 3))
                    {
                        positions[i] = new Vertex(x, y, z);
                        i++;
                    }
                }
            }

            Log.Info($"{positions.Length} rx positions created");
            return positions;
        }

        public override string ToString()
        {
            return $"Min: {MinCorner} Max: {MaxCorner}";
        }

        // --- properties

        public Vertex MinCorner { get; set; }
        public Vertex MaxCorner { get; set; }
        public float Resolution { get; set; }
        public int[] DataDimension { get; set; }
        public int TotalData { get; set; }
    }
}
