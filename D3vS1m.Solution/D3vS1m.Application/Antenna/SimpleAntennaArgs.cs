using D3vS1m.Domain.Data.Arguments;

namespace D3vS1m.Application.Antenna
{
    public class SimpleAntennaArgs : ArgumentsBase
    {

        public SimpleAntennaArgs()
        {
            Name = Models.SimpleAntenna;
        }

        public override ArgumentsBase GetDefault()
        {
            return new SimpleAntennaArgs();
        }

        /// <summary>
        /// Gets or sets the individual isotropic antenna gain for one device. 
        /// </summary>
        public float SimpleGain { get; set; }
    }
}
