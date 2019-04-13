using D3vS1m.Domain.Data.Arguments;

namespace D3vS1m.Application.Antenna
{
    public class FlatAntennaArgs : ArgumentsBase
    {
        public FlatAntennaArgs()
        {
            base.Name = "flat antenna model";
        }

        public override ArgumentsBase GetDefault()
        {
            return new FlatAntennaArgs();
        }

        /// <summary>
        /// Gets or sets the antenna gain for all devices to the same value. 
        /// </summary>
        public float FlatGain { get; set; }
    }
}
