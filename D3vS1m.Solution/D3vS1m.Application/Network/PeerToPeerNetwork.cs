using D3vS1m.Application.Devices;
using D3vS1m.Domain.Data.Repositories;
using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Network
{
    public class PeerToPeerNetwork : RepositoryBase<BasicDevice>
    {

        public PeerToPeerNetwork()
        {
            Name = Models.PeerToPeerNetwork;

            AssociationMatrix = new NetworkMatrix<bool>();
            DistanceMatrix = new NetworkMatrix<float>();
            RssMatrix = new NetworkMatrix<float>();
            AngleMatrix = new NetworkMatrix<Angle>();
        }

        // -- properties

        /// <summary>
        /// Gets or sets the communication associations between the devices.
        /// </summary>
        public NetworkMatrix<bool> AssociationMatrix { get; set; }

        /// <summary>
        /// Gets or sets the distances between the devices.
        /// </summary>
        public NetworkMatrix<float> DistanceMatrix { get; set; }

        /// <summary>
        /// Gets or sets the received signal strength (RSS) between the devices.
        /// </summary>
        public NetworkMatrix<float> RssMatrix { get; set; }

        /// <summary>
        /// Gets or sets the angles between the devices.
        /// </summary>
        public NetworkMatrix<Angle> AngleMatrix { get; set; }
    }
}
