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

        [TestMethod]
        public void ConvertContextToByteArray()
        {
            // arrange
            var factory = new D3vS1mFactory(new RuntimeController(new D3vS1mValidator()));
            factory.RegisterPredefined();

            // act
            var bytes = BinaryIO.ToBytes(factory);
            var result = BinaryIO.FromBytes<D3vS1mFactory>(bytes);

            // assert
            Assert.AreEqual(result.Simulators.Count, factory.Simulators.Count);
        }
    }
}
