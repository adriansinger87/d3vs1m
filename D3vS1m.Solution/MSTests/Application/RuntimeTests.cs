using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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

            _runtime = new RuntimeController();
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
            AdaptedFriisSimulator afs = new AdaptedFriisSimulator().With(new AdaptedFriisArgs()) as AdaptedFriisSimulator;
            afs.OnExecuting += AdaptedFriisOnExecuting;
            afs.Executed += AdaptedFriisExecuted;

            // arrange
            SimulatorRepository simRepo = new SimulatorRepository
            {
                new SceneSimulator().With(new InvariantSceneArgs()),
                afs,
                new SimpleAntennaSimulator().With(new SimpleAntennaArgs())
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

        private void AdaptedFriisOnExecuting(object sender, SimulatorEventArgs args)
        {

        }


        private void AdaptedFriisExecuted(object sender, SimulatorEventArgs args)
        {

        }
    }
}
