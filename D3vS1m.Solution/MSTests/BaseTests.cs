using D3vS1m.Application.Channel;
using D3vS1m.Application.Devices;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Logging;
using D3vS1m.Persistence;
using D3vS1m.Persistence.Imports;
using D3vS1m.Persistence.Logging;
using D3vS1m.Persistence.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                Log.Inject(new TestsNLogger());
            }
        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            Log.Stop();
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

        public List<BasicDevice> ImportDevices(string filename)
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
                return Path.Combine(BaseDirectory,"data");
            }
        }
    }
}
