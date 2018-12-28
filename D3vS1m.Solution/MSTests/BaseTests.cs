﻿using D3vS1m.Domain.System.Logging;
using D3vS1m.Persistence.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
