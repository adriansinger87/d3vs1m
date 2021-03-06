﻿using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Constants;
using System;

namespace D3vS1m.Application.Runtime
{
    [Serializable]
    public class RuntimeArgs : ArgumentsBase
    {
        public RuntimeArgs() : base()
        {
            Key = Models.Runtime.Key;
            Name = Models.Runtime.Name;
            CycleDuration = TimeSpan.FromSeconds(Const.Runtime.IncrementSeconds);
        }

        // -- methods

        public void ResetTime()
        {
            StartTime = DateTime.Now;
            SimulatedTime = new TimeSpan();
            Iterations = 0;
        }

        public override void Reset()
        {
            base.Guid = global::System.Guid.NewGuid().ToString();
            ResetTime();
        }

        // -- properties

        /// <summary>
        /// Gets or sets the local DateTime, when the simulation was started.
        /// </summary>
        public DateTime StartTime { get; set; }


        /// <summary>
        /// Gets or sets the theoretical realtime that has passed.
        /// </summary>
        public TimeSpan SimulatedTime { get; set; }

        /// <summary>
        /// Gets or sets the time the simulation was running.
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
