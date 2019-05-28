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
            var context = new D3vS1mFacade();
            context.RegisterPredefined(new RuntimeController(new D3vS1mValidator()));

            // act
            var bytes = BinaryIO.ToBytes(context);

            var result = BinaryIO.FromBytes<D3vS1mFacade>(bytes);

            // assert
            Assert.AreEqual(result.Simulators.Count, context.Simulators.Count);
        }
    }
}
