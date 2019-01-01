﻿using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests.Application
{
    [TestClass]
    public class RuntimeTests : BaseTests
    {
        // -- fields

        RuntimeBase _runtime;

        // -- inherits

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _runtime = new RuntimeController(new D3vS1mValidator());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        // -- test methods

        [TestMethod]
        public void RunSingleStep()
        {
            // arrange
            var sceneArgs = new InvariantSceneArgs();
            var radioArgs = base.GetRadioArgs();
            var comArgs = new WirelessCommArgs();
            var netArgs = new NetworkArgs();
            var antennaArgs = new SimpleAntennaArgs();

            SimulatorRepository simRepo = new SimulatorRepository
            {
                new SceneSimulator()
                    .With(sceneArgs),
                new AdaptedFriisSimulator()
                    .With(radioArgs)
                    .With(comArgs),
                new SimpleAntennaSimulator()
                    .With(antennaArgs),
                new PeerToPeerNetworkSimulator()
                    .With(netArgs)
            };

            // act
            if (_runtime.Setup(simRepo).Validate() == false)
            {
                Assert.Fail("error on validating the simulation");
            }

            _runtime.Run();
            _runtime.Stop();

            // assert

        }
    }
}
