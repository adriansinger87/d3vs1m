using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeleScope.Logging.Extensions;

namespace MSTests.Application
{
	[TestClass]
	public class NetworkTests : TestsBase
	{
		private PeerToPeerNetwork _network;

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
		public async Task RunNetwork()
		{
			// arrange
			var runtime = new RuntimeController(new BasicValidator());


			var netSim = new PeerToPeerNetworkSimulator(runtime);
			var netArgs = new NetworkArgs();
			netSim.OnExecuting += (o, e) =>
			{
				_network.AssociationMatrix.Each(ApplyAssociations);
			};
			netSim.With(netArgs);
			_network = netArgs.Network;
			_network.AddRange(ImportDevices().First());

			var simRepo = new SimulatorRepository();
			simRepo.Add(netSim);


			var pass = 0;
			var maxPasses = 3;
			runtime.IterationPassed += (o, e) =>
			{
				pass++;
				if (pass == maxPasses)
				{
					var dev = netArgs.Network.Items[0];
					dev.Controls.Off();
					_log.Trace($"Device '{dev.Name}' was turned off.");
				}
			};
			runtime.BindSimulators(simRepo)
				.Validate();

			// act iterations until availability is below 100%
			await runtime.RunAsync((r) =>
			{
				if (netArgs.Network.Availability() < 1)
				{
					return false;
				}
				else
				{
					return true;
				}
			});

			// assert
			Assert.IsTrue(pass >= maxPasses, $"The iterations should be more than {maxPasses}.");
			netArgs.Network.DistanceMatrix.Each((r, c, v) =>
			{
				Assert.IsTrue(v > 0, $"position at row '{r}' and col '{c}' should not be '{v}'");
				_log.Trace($"{r}:{c} -> distance: {v}");
				return v;
			});

			netArgs.Network.AngleMatrix.Each((r, c, v) =>
			{
				Assert.IsTrue(!float.IsNaN(v.Azimuth), $"Azimuth at position at row '{r}' and col '{c}' should not be NaN");
				Assert.IsTrue(!float.IsNaN(v.Elevation), $"Elevation at position at row '{r}' and col '{c}' should not be NaN");
				_log.Trace($"{r}:{c} -> angle: {v}");
				return v;
			});
		}

		public bool ApplyAssociations(int r, int c, bool v)
		{
			var dev1 = _network[r];
			var dev2 = _network[c];

			if ((dev1.Name.StartsWith("Anchor") && dev2.Name.StartsWith("Tag")) ||
				(dev1.Name.StartsWith("Tag") && dev2.Name.StartsWith("Anchor")))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
