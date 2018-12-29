using D3vS1m.Application.Channel;
using D3vS1m.Domain.Data.Scene;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
