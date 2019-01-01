using D3vS1m.Application.Channel;
using D3vS1m.Domain.Data.Scene;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Application.Communication;
using System.Diagnostics;
using D3vS1m.Domain.System.Logging;
using D3vS1m.Domain.Simulation;
using D3vS1m.Application.Scene;
using D3vS1m.Application.Network;

namespace MSTests.Application
{
    [TestClass]
    public class ChannelTests : BaseTests
    {
        // -- inherits

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

        // -- Tests

        [TestMethod]
        public void AdaptedFriis()
        {
            // arrange
            var watch = new Stopwatch();

            var comArgs = new WirelessCommArgs();
            var radioArgs = base.GetRadioArgs();
            var sceneArgs = new InvariantSceneArgs();
            var netArgs = new NetworkArgs();

            var sim = new AdaptedFriisSimulator()
                .With(radioArgs)                    // own arguments
                .With(comArgs)                      // additional arguments...
                .With(sceneArgs)
                .With(netArgs);                     // false positive

            sim.OnExecuting += (obj, e) => {
                Log.Trace($"{obj.ToString()} started");
            };

            sim.Executed += (obj, e) => {
                Log.Trace($"{obj.ToString()} finished");
            };

            // act
            watch.Start();
            sim.Run();
            watch.Stop();
            Log.Info($"{sim.Name} calculated {radioArgs.RadioBox.TotalData} values in {watch.ElapsedMilliseconds} ms");
            
            // assert
            Assert.IsTrue(radioArgs.RxValues.All(f => f != 0), "float should contain a attenuation"); 
        }

        [TestMethod]
        public void CreateRxPositions()
        {
            // arrange
            var box = new RadioCuboid();
            var min = new Vector(-10, -10, -10);
            var max = new Vector(10, 10, 10);

            box.Resolution = 0.25F;
            box.MinCorner = min;
            box.MaxCorner = max;

            // act
            var positions = box.CreateRxPositions();

            // assert
            Assert.IsTrue(positions.Length == box.TotalData, "total data does not match");
            Assert.IsTrue(positions.All(v => v != null), "vector is null");
        }
    }
}
