using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene.Materials;
using D3vS1m.Application.Validation;
using D3vS1m.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Repository;
using Sin.Net.Persistence.IO;
using Sin.Net.Persistence.Settings;
using System.Collections.Generic;

namespace MSTests.Persistence
{
    [TestClass]
    public class PersistenceTests : TestBase
    {
        IPersistenceControlable _io;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();
            _io = ArrangeIOController();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public void ExportMaterials()
        {
            var setting = new JsonSetting
            {
                Name = "material_config_export.json",
                Location = APP_LOCATION,
                Binder = JsonHelper.Binder
            };

            var result = _io.Exporter(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Build(Material.CreateDemoMaterials())
                .Export();

            Assert.IsTrue(!string.IsNullOrEmpty(result), "export should not be null.");
        }

        [TestMethod]
        public void ImportMaterials()
        {
            // arrange
            var setting = new JsonSetting
            {
                Name = "material_config.json",
                Location = APP_LOCATION,
                Binder = JsonHelper.Binder
            };

            // act

            var materials = _io.Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Import()
                .As<List<Material>>();

            // assert
            Assert.IsNotNull(materials, "Materials should be present");
            Assert.IsTrue(materials.Count > 0, "Materials should be more than zero.");
        }
    }
}
