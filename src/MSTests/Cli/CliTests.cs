using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSTests.Cli
{
    [TestClass]
    public class CliTests : TestsBase
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

        [TestMethod]
        public async Task RunProgram()
        {
            // arrange

            // act
            await D3vS1m.Cli.Program.Main(new string[] { });

            // assert
        }
    }
}
