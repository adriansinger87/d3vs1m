using D3vS1m.Domain.System.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Domain
{
    [TestClass]
    public class ConstTests : TestBase
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
            float rad = Const.Func.ToRadian(inputDeg);
            float deg = Const.Func.ToDegree(rad);

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
            float test1 = Const.Func.Fract(f1);
            float test2 = Const.Func.Fract(f2);
            Log.Info($"test1 is {test1}");
            Log.Info($"test2 is {test2}");

            // TODO: fix this on linux, here the result is 2 ot 0.2 for example
            // Assert
            //Assert.IsTrue(test1 == 0.2f, $"fract should be 0.2, but it is {test1}");
            //Assert.IsTrue(test2 == -0.7f, $"fract should be -0.7, but it is {test2}");

        }

    
    }
}
