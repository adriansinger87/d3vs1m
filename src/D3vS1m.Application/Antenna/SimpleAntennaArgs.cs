using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Application.Antenna
{
    [Serializable]
    public class SimpleAntennaArgs : ArgumentsBase
    {

        public SimpleAntennaArgs() : base()
        {
            Reset();
        }

        public override void Reset()
        {
            Name = Models.Antenna.Simple.Name;
        }

        /// <summary>
        /// Gets or sets the individual isotropic antenna gain for one device. 
        /// </summary>
        public float SimpleGain { get; set; }
    }
}
