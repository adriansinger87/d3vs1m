using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Application.Antenna
{
    [Serializable]
    public class FlatAntennaArgs : ArgumentsBase
    {
        public FlatAntennaArgs() : base()
        {
            base.Key = Models.Antenna.Flat.Key;
            base.Name = Models.Antenna.Flat.Name;
            Reset();
        }

        public override void Reset()
        {
            
        }

        /// <summary>
        /// Gets or sets the antenna gain for all devices to the same value. 
        /// </summary>
        public float FlatGain { get; set; }
    }
}
