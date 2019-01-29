using D3vS1m.Application.Network;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Application
{
    [TestClass]
    public class NetworkTests : BaseTests
    {

        PeerToPeerNetwork _network;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void RunNetwork()
        {
            // arrange
            var netSim = new PeerToPeerNetworkSimulator();
            var netArgs = new NetworkArgs();
            var devices = base.ImportDevices("devices.json").ToArray();
            _network = netArgs.Network;
            _network.AddRange(devices);

            // act
            netSim.OnExecuting += (o, e) =>
            {
                _network.SetupMatrices();
                _network.AssociationMatrix.Each(ApplyAssociations);
            };
            netSim.With(netArgs).Run();

            // assert
            netArgs.Network.DistanceMatrix.Each((r, c, v) => {

                Assert.IsTrue(v > 0, $"position at row '{r}' and col '{c}' should not be '{v}'");
                return v;
            });

            netArgs.Network.AngleMatrix.Each((r, c, v) => {

                Assert.IsTrue(float.IsNaN(v.Azimuth) == false, $"Azimuth at position at row '{r}' and col '{c}' should not be NaN");
                Assert.IsTrue(float.IsNaN(v.Elevation) == false, $"Elevation at position at row '{r}' and col '{c}' should not be NaN");
                return v;
            });
        }

        private bool ApplyAssociations(int r, int c, bool v)
        {
            var dev1 = _network[r];
            var dev2 = _network[c];

            if ((dev1.Name.StartsWith("Anchor") && dev2.Name.StartsWith("Tag")) ||
                (dev1.Name.StartsWith("Tag") && dev2.Name.StartsWith("Anchor")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
