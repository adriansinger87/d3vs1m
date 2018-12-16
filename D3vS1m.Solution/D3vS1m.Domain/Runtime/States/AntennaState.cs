using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.System.Logging;

namespace D3vS1m.Domain.Runtime.States
{
    internal class AntennaState : BaseRuntimeState
    {
        // -- construtor

        public AntennaState(RuntimeController control) : base(control)
        {
                
        }

        // -- methods
        
        public override void RunAntenna()
        {
            // TODO run usefull antenna model code here...
        }

        public override void RunChannel()
        {
            InvalidCall(State);
        }

        public override void RunCommunication()
        {
            InvalidCall(State);
        }

        public override void RunDevices()
        {
            InvalidCall(State);
        }

        public override void RunEnergy()
        {
            InvalidCall(State);
        }

        public override void RunNetwork()
        {
            InvalidCall(State);
        }

        public override void RunScene()
        {
            InvalidCall(State);
        }

        // -- properties

        public override RuntimeStates State { get { return RuntimeStates.Antenna; } }
    }
}
