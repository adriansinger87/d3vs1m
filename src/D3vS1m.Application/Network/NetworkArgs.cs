using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Application.Network
{
    [Serializable]
    public class NetworkArgs : ArgumentsBase
    {
        // -- constructor

        public NetworkArgs() : base()
        {
            Reset();
        }

        // -- methods

        public void ApplyAssociations()
        {

        }

        public override void Reset()
        {
            Name = Models.Network.Key;
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
