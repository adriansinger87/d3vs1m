using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.System.Logging
{
    /// <summary>
    /// static class for global access to the concrete log implementation
    /// </summary>
    public static class Log
    {
        // -- fields

        private static ILoggable _logger;
        private static readonly string NO_LOGGER_EXCEPTION = "no logger instance is present";

        // -- methods

        /// <summary>
        /// Takes the logger instance based on the ILoggable interface
        /// </summary>
        /// <param name="logger">an ILoggable implementation</param>
        public static void Inject(ILoggable logger)
        {
            _L = logger;
        }

        public static void Trace(string msg)
        {
            _L.Trace(msg);
        }

        public static void Debug(string msg)
        {
            _L.Debug(msg);
        }

        public static void Info(string msg)
        {
            _L.Info(msg);
        }

        public static void Warn(string msg)
        {
            _L.Warn(msg);
        }

        public static void Error(string msg)
        {
            _L.Error(msg);
        }

        public static void Fatal(Exception ex)
        {
            _L.Fatal(ex);
        }

        public static void Stop()
        {
            _L.Stop();
        }

        // -- properties

        /// <summary>
        /// Gets the information, if the logger instance is present or not.
        /// </summary>
        public static bool IsNotNull { get { return _logger != null; } }

        /// <summary>
        /// Gets a private shortcut to the local logger field with some validation and exception forwarding.
        /// </summary>
        private static ILoggable _L
        {
            get
            {
                if (_logger != null)
                {
                    return _logger;
                }
                else
                {
                    throw new NullReferenceException(NO_LOGGER_EXCEPTION);
                }
            }
            set
            {
                if (value != null)
                {
                    _logger = value;
                }
                else
                {
                    throw new NullReferenceException(NO_LOGGER_EXCEPTION);
                }
            }
        }
    }
}
