using D3vS1m.Application.Data;
using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Application.Antenna
{
    [Serializable]
    public class SphericAntennaArgs : ArgumentsBase
    {
        public SphericAntennaArgs()
        {
            Reset();
        }

        public override void Reset()
        {
            Name = Models.SphericAntenna;
        }

        public Matrix<SphericGain> GainMatrix { get; set; }
    }
}
