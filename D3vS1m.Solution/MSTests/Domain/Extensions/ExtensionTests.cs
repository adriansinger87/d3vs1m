using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Extensions;

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
            float rad = inputDeg.ToRadian();
            float deg = rad.ToDegree();

            // Assert
            Assert.IsTrue(Math.Round(inputDeg, 2) == Math.Round(deg, 2));

        }
    }
}
