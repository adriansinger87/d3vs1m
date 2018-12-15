using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.System.Logging
{
    public interface ILoggable
    {
        /// <summary>
        /// Starts up all the logging configurations and makes it ready to log messages.
        /// Call this method in the constructor for instant readiness.
        /// </summary>
        void Start();

        /// <summary>
        /// Shut down the logger and flush prending messages.
        /// </summary>
        void Stop();

        void Trace(string msg);
        void Debug(string msg);
        void Info(string msg);
        void Warn(string msg);
        void Error(string msg);
        void Fatal(Exception ex);
    }
}
