using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using D3vS1m.Domain.System.Constants;

namespace MSTests.Domain.Extensions
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void testRadDeg()
        {
            // Arrange
            float inputDeg = 45.5f;

            // Act
            float rad = Const.ToRadian(inputDeg);
            float deg = Const.ToDegree(rad);

            // Assert
            Assert.IsTrue(Math.Round(inputDeg, 2) == Math.Round(deg, 2));

        }
    }
}
