using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Persistence.IO;

namespace MSTests.Persistence
{
    [TestClass]
    public class PersistenceTests : TestBase
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
    }
}
