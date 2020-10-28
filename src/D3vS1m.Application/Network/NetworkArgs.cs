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
            Key = Models.Network.Key;
            Name = Models.Network.Name;
            Network = new PeerToPeerNetwork();

            Reset();
        }

        // -- methods

        public override void Reset()
        {
            
        }

        // -- properties

        public PeerToPeerNetwork Network { get; set; }

        /// <summary>
        /// Gets or sets the trigger for the connected simulator to recalculate the network data
        /// </summary>
        public bool NetworkOutdated { get; set; }
    }
}
