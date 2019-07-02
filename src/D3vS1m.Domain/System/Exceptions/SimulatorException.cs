using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace D3vS1m.Domain.System.Exceptions
{
    public class SimulatorException : Exception
    {
        public SimulatorException(ISimulatable simulator) : this(simulator, $"{simulator.Name} failed")
        {
        }

        public SimulatorException(ISimulatable simulator, string message) : base($"{simulator.Name} failed: {message}")
        {
        }

        public SimulatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
