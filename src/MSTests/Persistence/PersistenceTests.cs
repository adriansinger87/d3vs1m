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
using D3vS1m.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeleScope.Persistence.Json;

namespace MSTests.Persistence
{
	[TestClass]
	public class PersistenceTests : TestsBase
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

		// -- test methods
		
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
