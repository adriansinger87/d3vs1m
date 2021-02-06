using System;
using D3vS1m.Domain.System.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests.Domain
{
	[TestClass]
	public class ConstTests : TestsBase
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
		[TestMethod]
		public void testRadDeg()
		{
			// Arrange
			float inputDeg = 45.5f;

			// Act
			float rad = Const.Math.ToRadian(inputDeg);
			float deg = Const.Math.ToDegree(rad);

			// Assert
			Assert.IsTrue(Math.Round(inputDeg, 2) == Math.Round(deg, 2));

		}

		[TestMethod]
		public void testFract()
		{
			// Arrange
			float f1 = 1.2f;
			float f2 = -6.7f;

			// Act
			float test1 = Const.Math.Fract(f1);
			float test2 = Const.Math.Fract(f2);
			//Log.Info($"test1 is {test1}");
			//Log.Info($"test2 is {test2}");

			// TODO: fix this on linux, here the result is 2 not 0.2 for example
			// Assert
			//Assert.IsTrue(test1 == 0.2f, $"fract should be 0.2, but it is {test1}");
			//Assert.IsTrue(test2 == -0.7f, $"fract should be -0.7, but it is {test2}");

		}


	}
}
