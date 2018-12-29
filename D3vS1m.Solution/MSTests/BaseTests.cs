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
