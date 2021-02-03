using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene.Materials;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Extensions;
using D3vS1m.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests.Persistence
{
	[TestClass]
	public class PersistenceTests : TestBase
	{
		/*
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
		public async Task ExportParquet()
		{
			// arrange
			var setting = new FileSetting
			{
				Location = "App_Data",
				Name = "spheric-antenna-args.parquet",
			};

			var runtime = new RuntimeController(new D3vS1mValidator());
			var factory = new D3vS1mFactory();

			// load all relevant arguments
			var simArgs = factory.GetPredefinedArguments();
			var netArgs = simArgs.GetByName(Models.Network.Name) as NetworkArgs;
			netArgs.Index = 10;
			netArgs.Network.AddRange(
			   base.ImportDevices().ToArray());

			factory.SetupRuntime(simArgs, runtime);
			runtime.Simulators[SimulationTypes.Antenna].With(netArgs);
			if (runtime.Validate() == false)
			{
				Assert.Fail("error on validating the simulation");
			}
			await runtime.RunAsync(1);

			// act
			var result = new ParquetExporter()
				.Setup(setting)
				.Build(simArgs.ToList(), new SimArgsToParquetAdapter())
				.Export();

			// assert
			Assert.IsTrue(!string.IsNullOrEmpty(result), "There is no file result");
		}

		[TestMethod]
		public void MultipleWritesToParquet()
		{
			//var adapter = new 
			//var result = new ParquetExporter()
			//   .Setup(setting)
			//   .Build(simArgs.ToList(), new SimArgsToParquetAdapter())
			//   .Export();

			//// assert
			//Assert.IsTrue(!string.IsNullOrEmpty(result), "There is no file result");
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
		*/
	}
}
