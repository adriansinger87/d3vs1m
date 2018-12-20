using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Enumerations
{
    /// <summary>
    /// It describes the executed simulation model in the state machine
    /// </summary>
    public enum SimulationModels {
        Antenna,
        Channel,
        Devices,
        Netowrk,
        Communication,
        Energy,
        Scene,
        Custom
    }
}
