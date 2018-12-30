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

            var min = new Vector(-10, -10, -10);
            var max = new Vector(10, 10, 10);
            var comArgs = new WirelessCommArgs();
            var radioArgs = new AdaptedFriisArgs();

            radioArgs.RadioBox.Resolution = 0.25F;
            radioArgs.RadioBox.MinCorner = min;
            radioArgs.RadioBox.MaxCorner = max;
            // update the positions always when the box changes
            radioArgs.RxPositions = radioArgs.RadioBox.CreateRxPositions();

            var sim = new AdaptedFriisSimulator().With(radioArgs) as AdaptedFriisSimulator;
            sim.OnExecuting += (obj, e) => {
                (obj as AdaptedFriisSimulator).SetupCommunication(comArgs);
            };

            // act
            watch.Start();
            sim.Execute();
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
