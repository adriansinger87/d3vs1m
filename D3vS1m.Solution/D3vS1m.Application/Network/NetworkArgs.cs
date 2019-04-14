using D3vS1m.Domain.Data.Arguments;

namespace D3vS1m.Application.Network
{
    public class NetworkArgs : ArgumentsBase
    {
        // -- constructor

        public NetworkArgs()
        {
            Reset();
        }

        // -- methods

        public void ApplyAssociations()
        {

        }

        public override void Reset()
        {
            Name = Models.PeerToPeerNetwork;
            Network = new PeerToPeerNetwork();
        }

        // -- properties

        public PeerToPeerNetwork Network { get; set; }

        /// <summary>
        /// Gets or sets the trigger for the connected simulator to recalculate the network data
        /// </summary>
        public bool NetworkOutdated { get; set; }
    }
}
