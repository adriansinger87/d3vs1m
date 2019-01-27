using D3vS1m.Application.Devices;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using D3vS1m.Domain.System.Logging;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

using System.Text;

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

        public void ApplyAssociations()
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
