using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Runtime;
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

        RuntimeController _runtime;

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
            var simulators = new ISimulationRunnable[]
            {
                new BaseRunner(new SceneSimulator().With(new InvariantSceneArgs())),
                new BaseRunner(new AdaptedFriisSimulator().With(new AdaptedFriisArgs())),
                new BaseRunner(new SimpleAntennaSimulator().With(new SimpleAntennaArgs()))
            };

            _runtime.Setup(simulators);
            _runtime.Run();
            _runtime.Stop();
        }
    }
}
