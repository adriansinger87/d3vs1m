using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
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

        []

        [TestMethod]
        public void RunSingleStep()
        {
            // arrange
            AdaptedFriisSimulator afs = new AdaptedFriisSimulator().With(new AdaptedFriisArgs()) as AdaptedFriisSimulator;
            afs.OnExecuting += AdaptedFriisOnExecuting;
            afs.Executed += AdaptedFriisExecuted;

            SimulatorRepository simRepo = new SimulatorRepository
            {
                new SceneSimulator().With(new InvariantSceneArgs()),
                afs,
                new SimpleAntennaSimulator().With(new SimpleAntennaArgs()),
                new PeerToPeerNetworkSimulator().With(new NetworkArgs())
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
