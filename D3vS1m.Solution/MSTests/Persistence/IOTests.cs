using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence;
using D3vS1m.Persistence.Imports;
using D3vS1m.Persistence.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Persistence
{
    [TestClass]
    public class IOTests : BaseTests
    {
        IOControllable _io;
        private IOSettingsBase _setting;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _io = new IOController();

        }

        [TestCleanup]
        public override void Cleanup()
        {
            // cleanup
            base.Cleanup();
        }

        [TestMethod]
        public void ImportDevices()
        {
            // arrange, act
            int length = 18;
            var devices = base.ImportDevices("devices.json");

            var _setting = new FileSettings
            {
                Location = TestDataDirectory,
                Name = "devices_2.json"
            };
            

            // assert
            Assert.IsNotNull(devices);
            Assert.IsTrue(devices.Count == length, $"device list has length {devices.Count} , should be {length}");
            Assert.IsTrue(devices.TrueForAll(d => d != null), "device was 'null'");

            Assert.IsTrue(devices[0].Position.Equals(new Vertex(22.1F, 8.5F, 20F)), "device 0 has wrong position");
        }
    }
}
