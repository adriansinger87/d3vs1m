﻿using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Simulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Persistence.Logging;
using System.Threading.Tasks;

namespace MSTests.Application
{
	[TestClass]
	public class NetworkTests : TestBase
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
			_network.AddRange(ImportDevices().ToArray());

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
					Log.Trace($"Device '{dev.Name}' was turned off.");
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
				Log.Trace($"{r}:{c} -> distance: {v}");
				return v;
			});

			netArgs.Network.AngleMatrix.Each((r, c, v) =>
			{
				Assert.IsTrue(float.IsNaN(v.Azimuth) == false, $"Azimuth at position at row '{r}' and col '{c}' should not be NaN");
				Assert.IsTrue(float.IsNaN(v.Elevation) == false, $"Elevation at position at row '{r}' and col '{c}' should not be NaN");
				Log.Trace($"{r}:{c} -> angle: {v.ToString()}");
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
