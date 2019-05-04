using D3vS1m.Domain.System.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Domain
{
    [TestClass]
    public class ConstTests
    {
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

            var t1_1 = f1 - (float)Math.Truncate(f1);

            // Assert
            Assert.IsTrue(test1 == 0.2f, "fract should be 0.2");
            Assert.IsTrue(test2 == -0.7f, "fract should be -0.7");

        }
    }
}
