using D3vS1m.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Runtime.States
{
    internal interface ISimulationRunnable
    {
        void RunScene();

        void RunDevices();

        void RunCommunication();

        void RunEnergy();

        void RunNetwork();

        void RunAntenna();

        void RunChannel();

        // -- properties

        RuntimeStates State { get; }

    }
}
