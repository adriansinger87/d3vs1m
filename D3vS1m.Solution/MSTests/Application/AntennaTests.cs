using D3vS1m.Application.Antenna;
using D3vS1m.Application.Data;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence;
using D3vS1m.Persistence.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSTests.Application
{
    [TestClass]
    public class AntennaTests : BaseTests
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
            LoadCsv();

            antennaSim.With(_antennaArgs);

            // act
            antennaSim.Run();

            // assert
            Assert.IsNotNull(antennaSim.Arguments, $"args '{_antennaArgs.Name}' must not be null in simulator '{antennaSim.Name}'");

        }

        // TODO remove refactore into another test
        private void LoadCsv()
        {
            var io = new IOController();

            var settings = new CsvSettings
            {
                Location = base.DataDirectory,
                Name = "PCB_868_tot.csv",
            };

            Matrix<SphericGain> csv = io
                .Importer(ImportTypes.Csv)
                .Setup(settings)
                .Import()
                .CastTo<Matrix<SphericGain>>(new SphericAntennaCasting());
        }
    }
}
