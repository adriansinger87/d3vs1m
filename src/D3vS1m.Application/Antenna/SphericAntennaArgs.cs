using D3vS1m.Application.Data;
using D3vS1m.Domain.Data.Arguments;
using System;
using System.Linq;
using TeleScope.Persistence.Abstractions;

namespace D3vS1m.Application.Antenna
{
    [Serializable]
    public class SphericAntennaArgs : ArgumentsBase
    {
        // -- properties

        public Matrix<SphericGain> GainMatrix { get; set; }

        public string DataSource { get; set; }

        // --constructor

        public SphericAntennaArgs() : base()
        {
            Key = Models.Antenna.Spheric.Key;
            Name = Models.Antenna.Spheric.Name;
        }

        // -- methods

        public override void Reset()
        {
            DataSource = string.Empty;
            GainMatrix = null;
        }
    }
}
