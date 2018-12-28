using D3vS1m.Domain.Data.Arguments;

namespace D3vS1m.Application.Antenna
{
    public class SimpleAntennaArgs : ArgumentsBase
    {
        public SimpleAntennaArgs()
        {
            base.Name = Models.SimpleAntenna;
        }

        /// <summary>
        /// Gets or sets the individual isotropic antenna gain for one device. 
        /// </summary>
        public float SimpleGain { get; set; }
    }
}
