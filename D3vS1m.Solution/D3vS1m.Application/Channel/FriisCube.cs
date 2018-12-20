using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Channel
{
    internal class FriisCube
    {
        public FriisCube()
        {
            this.MinCorner = new Vector();
            this.MaxCorner = new Vector();
            this.Resolution = 1;
        }

        // --- public methoden

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
