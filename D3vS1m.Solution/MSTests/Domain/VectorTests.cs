using D3vS1m.Domain.Data.Scene;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Domain
{
    [TestClass]
    public class VectorTests
    {

        [TestMethod]
        public void TestVectorMethods()
        {
            var a = new Vector();
            var b = new Vector(10, 0, 0);

            // Length
            float len = b.Length;
            Assert.IsTrue(Vector.GetLength(a, b) == len);

            // operators
            a.Set(10, 0, 0);
            var c = a * b;  // cross product new vector c
            Assert.IsTrue((b - a).Length == 0);
            Assert.IsTrue((a + b).Length == 20);
            Assert.IsTrue(Vector.GetLength(a, b) == 0);
            Assert.IsTrue(c.Length == 0);

            // Normalize
            a.Set(0, 0, 10);
            var n = Vector.Normalize(a, b, c);
            Assert.IsTrue(n.Length == 1);
            Assert.IsTrue(n.X == 0 && n.Y != 0 && n.Z == 0);

        }
    }
}
