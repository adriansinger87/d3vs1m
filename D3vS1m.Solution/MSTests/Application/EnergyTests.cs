using D3vS1m.Application.Energy;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSTests.Application
{
    [TestClass]
    public class EnergyTests : BaseTests
    {
        BatteryPack _battery;

       [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _battery = new BatteryPack();
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

            // act
            int sec = 3;
            _battery.State.Now.AddTime(new TimeSpan(0, 0, sec));

            // assert
            Assert.IsNotNull(_battery, "battery object should not be null");
            Assert.IsTrue(_battery.State.Initial.Charge > 0 && _battery.State.Now.Charge == 0, "charge of the battery is not valid");
            Assert.IsTrue(_battery.State.Initial.ElapsedTime.TotalSeconds == 0, $"battery should be initialized with no elapsed time");
            Assert.IsTrue(_battery.State.Now.ElapsedTime.TotalSeconds >= sec, $"battery should be used at leased {sec} seconds");
        }

        [TestMethod]
        public async Task RunBatterySimulation()
        {
            // arrange
            var runtime = new RuntimeController(new EnergyValidator());

            // TODO: seem to have a but when setting up a higher cutoff voltage --> test and fix it
            _battery.CutoffVoltage = 1.2F;
            _battery.State.Init(_battery);
            BatteryState s = _battery.State;

            var batteryArgs = new BatteryArgs();
            batteryArgs.Batteries.Add(_battery);

            var batterySim = new BatteryPackSimulator();
            batterySim
              .With(batteryArgs)
              .With(runtime.Arguments);

            var repo = new SimulatorRepository();
            repo.Add(batterySim);
            if (runtime.Setup(repo).Validate() == false)
            {
                Assert.Fail("error on validating the simulation");
            }

            // act
            await runtime.RunAsync((rt) => {

                // assert
                return !_battery.State.IsDepleted;
            });

            Assert.AreNotEqual(s.Initial.ElapsedTime, s.Now.ElapsedTime, "ElapsedTime should not be equal");
            Assert.AreNotEqual(s.Initial.Charge, s.Now.Charge, "Charge should not be equal");
            Assert.AreNotEqual(s.Initial.SoD, s.Now.SoD, "SoD should not be equal");
            Assert.AreNotEqual(s.Initial.Voltage, s.Now.Voltage, "Voltage should not be equal");
            Assert.IsTrue(_battery.State.IsDepleted == true, "battery should be empty");
        }
    }
}
