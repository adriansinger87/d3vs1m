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
        public void SetupNetwork()
        {
            // arrange
            var netArgs = new NetworkArgs();
            var devices = base.ImportDevices("devices.json").ToArray();

            netArgs.AddDevices(devices);

            // act
            netArgs.SetupMatrices();
            netArgs.CalculateDistances();

            // assert
            netArgs.Network.DistanceMatrix.Each((r, c, v) => {

                Assert.IsTrue(v > 0, $"position at row '{r}' and col '{c}' should not be '{v}'");
                return v;
            });
        }
    }
}
