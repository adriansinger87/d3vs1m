using D3vS1m.Domain.Data.Arguments;

namespace D3vS1m.Application.Network
{
    public class NetworkArgs : ArgumentsBase
    {
        // -- constructor

        public NetworkArgs()
        {
            Name = Models.PeerToPeerNetwork;
            Network = new PeerToPeerNetwork();
        }

        // -- methods

        public void ApplyAssociations()
        {

        }

        public override ArgumentsBase GetDefault()
        {
            return new NetworkArgs();
        }

        // -- properties

        public PeerToPeerNetwork Network { get; set; }

        /// <summary>
        /// Gets or sets the trigger for the connected simulator to recalculate the network data
        /// </summary>
        public bool NetworkOutdated { get; set; }
    }
}
