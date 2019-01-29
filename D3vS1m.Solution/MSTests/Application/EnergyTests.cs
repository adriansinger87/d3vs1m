using D3vS1m.Application.Energy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Application
{
    [TestClass]
    public class EnergyTests : BaseTests
    {
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

        [TestMethod]
        public void SetupBatteryPack()
        {
            // arrange
            var battery = new BatteryPack();

            // act
            int sec = 3;
            battery.State.Now.AddTime(new TimeSpan(0, 0, sec));

            // assert
            Assert.IsNotNull(battery, "battery object should not be null");
            Assert.IsTrue(battery.State.Initial.Charge > 0 && battery.State.Now.Charge == 0, "charge of the battery is not valid");
            Assert.IsTrue(battery.State.Initial.ElapsedTime.TotalSeconds == 0, $"battery should be initialized with no elapsed time");
            Assert.IsTrue(battery.State.Now.ElapsedTime.TotalSeconds >= sec, $"battery should be used at leased {sec} seconds");
        }
    }
}
