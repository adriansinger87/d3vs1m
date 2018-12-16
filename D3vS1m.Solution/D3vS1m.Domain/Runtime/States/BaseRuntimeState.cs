using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.System.Logging;

namespace D3vS1m.Domain.Runtime.States
{
    internal abstract class BaseRuntimeState : ISimulationRunnable
    {
        // -- fields

        protected const string RUNTIME_ERROR = "wrong runtime method called";

        internal RuntimeController _control;

        // -- constructor

        public BaseRuntimeState(RuntimeController control)
        {
            _control = control;
        }

        // -- methods

        public abstract void RunAntenna();

        public abstract void RunChannel();

        public abstract void RunCommunication();

        public abstract void RunDevices();

        public abstract void RunEnergy();

        public abstract void RunScene();

        public abstract void RunNetwork();

        /// <summary>
        /// default error handling for calling the wrong method in a certain runtime state
        /// </summary>
        /// <param name="callingState">the state of the </param>
        public void InvalidCall(RuntimeStates callingState)
        {
            Log.Error($"{RUNTIME_ERROR}, calling state '{callingState}' in state '{_control.CurrentState}'");
        }

        // -- properties

        public abstract RuntimeStates State { get; }
    }
}
