using D3vS1m.Application.Channel;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Logging;
using D3vS1m.Persistence.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        public AdaptedFriisArgs GetRadioArgs()
        {
            var min = new Vector(-10, -10, -10);
            var max = new Vector(10, 10, 10);
            var radioArgs = new AdaptedFriisArgs();

            radioArgs.RadioBox.Resolution = 0.25F;
            radioArgs.RadioBox.MinCorner = min;
            radioArgs.RadioBox.MaxCorner = max;
            // update the positions always when the box changes
            radioArgs.RxPositions = radioArgs.RadioBox.CreateRxPositions();

            return radioArgs;
        }

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

        public string DataDirectory
        {
            get
            {
                return Path.Combine(BaseDirectory,"data");
            }
        }
    }
}
