using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Data
{
    public class SphericGain : Angle
    {
        // --- constructor

        public SphericGain()
        {
        }

        public override string ToString()
        {
            return $"{Gain} dBi @ {base.ToString()}";
        }

        // --- properties

        public float Gain { get; set; }
    }
}
