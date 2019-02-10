﻿using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSTests.Application
{
    [TestClass]
    public class RuntimeTests : BaseTests
    {
        // -- fields

        RuntimeBase _runtime;
        SimulatorRepository _repo;

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

            _repo = new SimulatorRepository
            {
                new SceneSimulator(runtime)
                    .With(sceneArgs),
                new AdaptedFriisSimulator(runtime)
                    .With(radioArgs)
                    .With(comArgs),
                new SimpleAntennaSimulator(runtime)
                    .With(antennaArgs),
                new PeerToPeerNetworkSimulator(runtime)
                    .With(netArgs)
            };
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
            var facade = new D3vS1mFacade();
            facade.RegisterPredefined(_runtime);
            var netArgs = facade.SimulatorRepo[SimulationModels.Network].Arguments as NetworkArgs;

            // special setup
            _runtime.Started += (o, e) =>
            {
                facade.SimulatorRepo[SimulationModels.Channel].With(base.GetRadioArgs());
            };

            netArgs.Network.AddRange(
                base.ImportDevices().ToArray());

            // act
            int iterations = 5;
            if (_runtime.Setup(facade.SimulatorRepo).Validate() == false)
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
            var facade = new D3vS1mFacade();

            // act
            facade.RegisterPredefined(_runtime);

            // assert
            Assert.IsTrue(facade.SimulatorRepo.Count >= 4, "not enough simulators registered");
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
