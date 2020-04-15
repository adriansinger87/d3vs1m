using D3vS1m.Application.Scene.Materials;
using D3vS1m.Persistence;
using D3vS1m.Persistence.Exports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Persistence;
using Sin.Net.Persistence.Settings;
using System.Collections.Generic;

namespace MSTests.Persistence
{
    [TestClass]
    public class PersistenceTests : TestBase
    {
        private IPersistenceControlable _io;

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

        // -- test methods

        [TestMethod]
        public void ExportParquet()
        {
            // arrange
            var setting = new FileSetting
            {
                Location = "App_Data",
                Name = "demo.parquet",
            };

            // act
            var result = new ParquetExporter()
                .Setup(setting)
                .Export();

            // assert
            Assert.IsTrue(!string.IsNullOrEmpty(result), "There is no file result");
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
                .Build(CreateDemoMaterials())
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

        protected static List<Material> CreateDemoMaterials()
        {
            var freq = 2405.0F;
            var list = new List<Material>
            {
                new Material("Beton", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 3,
                    RelativePermeability = 1.0F,
                    RelativePermittivity = 9.0F }
                    .CalcReflectionValues()),
                new Material("Gips", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 1.5F,
                    RelativePermeability = 1,
                    RelativePermittivity = 1 }
                    .CalcReflectionValues()),
                new Material("Holzfassade", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 10.0F,
                    RelativePermeability = 1.0F,
                    RelativePermittivity = 2.3345F }
                    .CalcReflectionValues()),
                new Material("Holz mittel", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 3.44F,
                    RelativePermeability = 1,
                    RelativePermittivity = 1.5905F }
                    .CalcReflectionValues()),
                new Material("Holz dünn", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 1.5F,
                    RelativePermeability = 1,
                    RelativePermittivity = 2.33F }
                    .CalcReflectionValues()),
                new Material("Stahl", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 7.0F,
                    RelativePermeability = 100000.0F,
                    RelativePermittivity = 2.3345F }
                    .CalcReflectionValues()),
                new Material("Metall Kleinteile", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 7,
                    RelativePermeability = 1,
                    RelativePermittivity = 100000.0F }
                    .CalcReflectionValues()),
                new Material("Aussenglas", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 0.77F,
                    RelativePermeability = 1,
                    RelativePermittivity = 6 }
                    .CalcReflectionValues()),
                new Material("Innenglas", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 0.77F,
                    RelativePermeability = 1,
                    RelativePermittivity = 19 }
                    .CalcReflectionValues())
            };

            return list;
        }
    }
}
