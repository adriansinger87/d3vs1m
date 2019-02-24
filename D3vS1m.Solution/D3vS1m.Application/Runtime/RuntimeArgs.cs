using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Runtime
{
    public class RuntimeArgs : ArgumentsBase
    {
        public RuntimeArgs()
        {
            Name = Models.Runtime;
            CycleDuration = TimeSpan.FromSeconds(Const.Runtime.IncrementSeconds);
        }

        // -- methods

        public void ResetTime()
        {
            StartTime = DateTime.Now;
            ElapsedTime = new TimeSpan();
            Iterations = 0;
        }

        // -- properties

        /// <summary>
        /// Gets or sets the local DateTime, when the simulation was started.
        /// </summary>
        public DateTime StartTime { get; set; }


        /// <summary>
        /// Gets or sets the theoretical realtime that has passed.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets the number of iterations the runtime has completed. 
        /// </summary>
        public ulong Iterations { get; set; }

        /// <summary>
        /// Gets or sets the duration of one iteration of the runtime.
        /// </summary>
        public TimeSpan CycleDuration { get; set; }
    }
}
