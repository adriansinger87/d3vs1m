using System.Linq;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Network;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Scene;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeleScope.Logging.Extensions;

namespace MSTests.Application
{
	[TestClass]
	public class ChannelTests : TestsBase
	{
		// -- inherits

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

		// -- Tests

		[TestMethod]
		public void AdaptedFriis()
		{
			// arrange
			var comArgs = new WirelessCommArgs();
			var radioArgs = base.GetRadioArgs();
			var sceneArgs = new InvariantSceneArgs();
			var netArgs = new NetworkArgs();

			// TODO: add the runtime and secure the existence of needed arguments commArgs and sceneArgs
			var sim = new AdaptedFriisSimulator()
				.With(radioArgs)                    // own arguments
				.With(comArgs)                      // additional arguments...
				.With(sceneArgs)
				.With(netArgs);                     // false positive

			sim.OnExecuting += (obj, e) =>
			{
				_log.Trace($"{obj} started");
			};

			sim.Executed += (obj, e) =>
			{
				_log.Trace($"{obj} finished");
			};

			// act
			sim.Run();

			// assert
			Assert.IsTrue(radioArgs.RxValues.All(f => f != 0), "float should contain a attenuation");

			radioArgs.RxPositions
				.Zip(radioArgs.RxValues, (a, b) => $"Pos: {a.ToString()} with {b} dBm")
				.ToList();
		}

		[TestMethod]
		public void CreateRxPositions()
		{
			// arrange
			var box = new RadioCuboid();
			var min = new Vertex(-10, -10, -10);
			var max = new Vertex(10, 10, 10);

			box.Resolution = 0.25F;
			box.MinCorner = min;
			box.MaxCorner = max;

			// act
			var positions = box.CreateRxPositions();

			// assert
			Assert.IsTrue(positions.Length == box.TotalData, "total data does not match");
			Assert.IsTrue(positions.All(v => v != null), "vector is null");
		}
	}
}
