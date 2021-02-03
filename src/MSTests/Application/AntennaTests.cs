using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MSTests.Application
{
    [TestClass]
    public class AntennaTests : TestBase
    {
        SphericAntennaArgs _antennaArgs;

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
        public void SimulateSphericAntenna()
        {
            // arrange

            var antennaSim = new SphericAntennaSimulator();
            _antennaArgs = new SphericAntennaArgs();
            LoadAntennaData(_antennaArgs);
            
            WriteJson(_antennaArgs, "spheric_antenna_args.json");
            antennaSim.With(_antennaArgs);


            // act
            antennaSim.Run();

            // assert
            Assert.IsNotNull(antennaSim.Arguments, $"args '{_antennaArgs.Name}' must not be null in simulator '{antennaSim.Name}'");
        }
    }
}
