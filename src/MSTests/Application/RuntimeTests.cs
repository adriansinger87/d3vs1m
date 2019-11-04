using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Threading.Tasks;

namespace MSTests.Application
{
    [TestClass]
    public class RuntimeTests : TestBase
    {
        // -- fields

        private RuntimeBase _runtime;
        private SimulatorRepository _repo;

        // -- inherits

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _runtime = new RuntimeController(new D3vS1mValidator());
            _runtime.IterationPassed += (o, e) =>
            {
                var runtimeArgs = e.Arguments as RuntimeArgs;
                var duration = (DateTime.Now - runtimeArgs.StartTime);
                Log.Trace($"'{o.ToString()}' passed one iteration after {duration}");
            };

            //SetupDemoRepo(_runtime);
        }

        private void SetupDemoRepo(RuntimeBase runtime)
        {
            var sceneArgs = new InvariantSceneArgs();
            var radioArgs = base.GetRadioArgs();
            var comArgs = new WirelessCommArgs();
            var netArgs = new NetworkArgs();
            var antennaArgs = new SimpleAntennaArgs();

            _repo = new SimulatorRepository();
            _repo.AddRange(new ISimulatable[] {
                new SceneSimulator(runtime)
                    .With(sceneArgs),
                new AdaptedFriisSimulator(runtime)
                    .With(radioArgs)
                    .With(comArgs),
                new SimpleAntennaSimulator(runtime)
                    .With(antennaArgs),
                new PeerToPeerNetworkSimulator(runtime)
                    .With(netArgs),
                new LRWPANSimulator(runtime)
                    .With(comArgs)
            });
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        // -- test methods

        [TestMethod]
        public async Task RunPredefinedSimulation()
        {
            // arrange

            // create the simulation-factory
            var factory = new D3vS1mFactory();

            // load all relevant arguments
            var simArgs = factory.GetPredefinedArguments();

            // setup radio channel
            var radioArgs = simArgs.GetByName(Models.Channel.AdaptedFriis.Key) as AdaptedFriisArgs;
            var min = new Vertex(-10, -10, -10);
            var max = new Vertex(10, 10, 10);
            radioArgs.RadioBox.Resolution = 0.25F;
            radioArgs.RadioBox.MinCorner = min;
            radioArgs.RadioBox.MaxCorner = max;

            // fill antenna data
            var antennaArgs = simArgs.GetByName(Models.Antenna.Spheric.Key) as SphericAntennaArgs;
            base.LoadAntennaData(antennaArgs);
            
            // fill network data
            var netArgs = simArgs.GetByName(Models.Network.Key) as NetworkArgs;
            netArgs.Network.AddRange(
               base.ImportDevices().ToArray());

            // final setup, cross-bind some arguments
            var runtime = factory.SetupSimulation(simArgs, _runtime);
            _runtime.Started += (o, e) =>
            {
            };
            
            // act
            int iterations = 5;
            if (runtime.Validate() == false)
            {
                Assert.Fail("error on validating the simulation");
            }

            Log.Trace($"RunAsync for {iterations} times");
            await _runtime.RunAsync(iterations);

            // assert
            var runArgs = _runtime.Arguments as RuntimeArgs;
            Assert.IsTrue(runArgs.Iterations == 5, $"simulation should have passed {iterations} iterations");
        }
    }
}
