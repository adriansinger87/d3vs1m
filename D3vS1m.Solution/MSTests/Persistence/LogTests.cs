using D3vS1m.Domain.System.Logging;
using D3vS1m.Persistence.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MSTests.Persistence
{
    [TestClass]
    public class LogTests
    {
        [TestInitialize]
        public void Arrange()
        {
            Log.Inject(new NLogger());
        }

        [TestCleanup]
        public void Cleanup()
        {
            // cleanup
        }

        [TestMethod]
        public void TestNLogger()
        {
            Assert.IsTrue(Log.IsNotNull, "logger should be injected");

            Log.Trace("eine Trace Nachricht");
            Log.Debug("eine Debug Nachricht");
            Log.Info("eine Info Nachricht");
            Log.Warn("eine Warn Nachricht");
            Log.Error("eine Error Nachricht");
            Log.Fatal(new Exception("eine Fatal Exception"));
        }

    }
}
