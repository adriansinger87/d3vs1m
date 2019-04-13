using D3vS1m.Application.Data;
using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Antenna
{
    public class SphericAntennaArgs : ArgumentsBase
    {
        public SphericAntennaArgs()
        {
            Name = Models.SphericAntenna;
        }

        public override ArgumentsBase GetDefault()
        {
            return new SphericAntennaArgs();
        }

        public Matrix<SphericGain> GainMatrix { get; set; }
    }
}
