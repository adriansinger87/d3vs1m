using D3vS1m.Application.Devices;
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
        IOController _io;
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
            // arrange
            _setting = new FileSettings
            {
                Location = base.DataDirectory,
                Name = "devices.json"
            };
            int length = 18;

            // act
            List<BasicDevice> devices = _io.Importer(ImportTypes.Json)
                .Setup(_setting)
                .Import()
                .CastTo<List<BasicDevice>>(new JsonCasting());

            // assert
            Assert.IsNotNull(devices);
            Assert.IsTrue(devices.Count == length, $"device list has length {devices.Count} , should be {length}");
            Assert.IsTrue(devices.TrueForAll(d => d != null), "device was 'null'");
        }
    }
}
