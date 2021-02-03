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
        public SphericAntennaArgs() : base()
        {
            Key = Models.Antenna.Spheric.Key;
            Name = Models.Antenna.Spheric.Name;
            Reset();
        }

        public override void Reset()
        {
           
        }

        public void LoadData(IReadable<Matrix<SphericGain>> reader)
        {
            GainMatrix = reader.Read().First();
        }

        public Matrix<SphericGain> GainMatrix { get; set; }

        public string DataSource { get; set; }
    }
}
