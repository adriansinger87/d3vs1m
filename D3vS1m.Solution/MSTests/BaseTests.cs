using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Data;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence;
using D3vS1m.Persistence.Imports;
using D3vS1m.Persistence.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Logging;
using Sin.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace MSTests
{
    public abstract class BaseTests
    {
        [TestInitialize]
        public virtual void Arrange()
        {
            if (Log.IsNotNull == false)
            {
                Log.Inject(new NLogger());
            }
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Log.Stop();
        }

        public void LoadAntennaData(SphericAntennaArgs args, string file = "PCB_868_tot.csv")
        {
            var io = new IOController();

            var settings = new CsvSettings
            {
                Location = DataDirectory,
                Name = file,
            };

            args.GainMatrix = io
                .Importer(ImportTypes.Csv)
                .Setup(settings)
                .Import()
                .CastTo<Matrix<SphericGain>>(new SphericAntennaCasting());
        }

        public RuntimeController GetRuntime()
        {
            var runtime = new RuntimeController(new D3vS1mValidator());

            return runtime;
        }

        public AdaptedFriisArgs GetRadioArgs()
        {
            var min = new Vertex(-10, -10, -10);
            var max = new Vertex(10, 10, 10);
            var radioArgs = new AdaptedFriisArgs();

            radioArgs.RadioBox.Resolution = 0.25F;
            radioArgs.RadioBox.MinCorner = min;
            radioArgs.RadioBox.MaxCorner = max;
            // update the positions always when the box changes
            radioArgs.RxPositions = radioArgs.RadioBox.CreateRxPositions();

            return radioArgs;
        }

        public List<BasicDevice> ImportDevices(string filename = "devices.json")
        {
            // arrange
            var _setting = new FileSettings
            {
                Location = TestDataDirectory,
                Name = filename //"devices.json"
            };


            IOControllable io = new IOController();
            // act
            List<BasicDevice> devices = io.Importer(ImportTypes.Json)
                .Setup(_setting)
                .Import()
                .CastTo<List<BasicDevice>>(new JsonCasting());

            return devices;
        }

        // -- properties

        public string BaseDirectory
        {
            get
            {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                dir = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory.Replace(@"\MSTests\bin\Debug\netcoreapp2.1", ""),
                    @"D3vS1m.Web\wwwroot");

                return dir;

            }
        }

        public string TestDataDirectory
        {
            get
            {
                return "Test_Data";
            }
        }
        public string DataDirectory
        {
            get
            {
                return Path.Combine(BaseDirectory, "data");
            }
        }
    }
}
