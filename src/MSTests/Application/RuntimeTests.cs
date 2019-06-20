using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Logging;
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

            SetupDemoRepo(_runtime);
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
            var factory = new D3vS1mFactory(_runtime);
            factory.RegisterPredefined();
            var antennaArgs = factory.Simulators.GetByName(Models.Antenna.Spheric.Key).Arguments as SphericAntennaArgs;
            base.LoadAntennaData(antennaArgs);
            var netArgs = factory.Simulators[SimulationModels.Network].Arguments as NetworkArgs;
            netArgs.Network.AddRange(
               base.ImportDevices().ToArray());
            factory.Simulators[SimulationModels.Network].With(netArgs);

            // special setup
            _runtime.Started += (o, e) =>
            {
                factory.Simulators[SimulationModels.Channel].With(base.GetRadioArgs());
            };
            
            // act
            int iterations = 5;
            if (_runtime.Setup(factory.Simulators).Validate() == false)
            {
                Assert.Fail("error on validating the simulation");
            }

            Log.Trace($"RunAsync for {iterations} times");
            await _runtime.RunAsync(iterations);

            // assert
            var runArgs = _runtime.Arguments as RuntimeArgs;
            Assert.IsTrue(runArgs.Iterations == 5, $"simulation should have passed {iterations} iterations");
        }

        [TestMethod]
        public void RegisterSimulation()
        {
            // arrange
            var facade = new D3vS1mFactory(_runtime);

            // act
            facade.RegisterPredefined();

            // assert
            Assert.IsTrue(facade.Simulators.Count >= 4, "not enough simulators registered");
        }

        [TestMethod]
        public async Task RunAsync()
        {
            // arrange
            int timing;
            if (_runtime.Setup(_repo).Validate() == false)
            {
                Assert.Fail("error on validating the simulation");
            }

            // act 3 iterations
            timing = 3;
            Log.Trace($"RunAsync for {timing} times");
            await _runtime.RunAsync(timing);

            // act 3 seconds of running simulation
            timing = 3000;
            Log.Trace($"RunAsync for {timing} ms");
            await _runtime.RunAsync((runtime) =>
            {
                var args = runtime.Arguments as RuntimeArgs;
                return (DateTime.Now - args.StartTime).TotalMilliseconds <= timing ? true : false;
            });

        }
    }
}
