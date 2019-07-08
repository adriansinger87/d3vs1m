using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Domain.Events
{
    public delegate void SimulatorEventHandler(object sender, SimulatorEventArgs e);
    
    public class SimulatorEventArgs : EventArgs
    {
        // -- constructor

        public SimulatorEventArgs(ArgumentsBase arguments)
        {
            Timestamp = DateTime.Now;
            Arguments = arguments;
        }

        // -- properties

        public DateTime Timestamp { get; private set; }

        public ArgumentsBase Arguments { get; private set; }
    }
}
