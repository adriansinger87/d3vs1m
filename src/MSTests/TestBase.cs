using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Data;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Persistence;
using D3vS1m.Persistence.Imports;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Logging;
using Sin.Net.Persistence;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.IO;

namespace MSTests
{
    public abstract class TestBase
    {
        // -- fields

        protected const string APP_LOCATION = "App_Data";

        // -- basic methods


        [TestInitialize]
        public virtual void Arrange()
        {
            if (Log.IsNotNull == false)
            {
                Log.Inject(new TestLogger().Start());
            }
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Log.Stop();
        }

        protected IPersistenceControlable ArrangeIOController()
        {
            var io = new PersistenceController();
            io.Add(D3vS1m.Persistence.Constants.Wavefront.Key, new ObjImporter());

            return io; 
        }

        public void LoadAntennaData(SphericAntennaArgs args, string file = "PCB_868_tot.csv")
        {
            var io = ArrangeIOController();

            var settings = new CsvSetting
            {
                Location = APP_LOCATION,
                Name = file,
            };

            args.GainMatrix = io
                .Importer(Sin.Net.Persistence.Constants.Csv.Key)
                .Setup(settings)
                .Import()
                .As<Matrix<SphericGain>>(new TableToAntennaAdapter());
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
            var _setting = new JsonSetting
            {
                Location = APP_LOCATION,
                Name = filename
            };

            IPersistenceControlable io = new PersistenceController();
            // act
            List<BasicDevice> devices = io.Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(_setting)
                .Import()
                .As<List<BasicDevice>>();

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
    }
}
