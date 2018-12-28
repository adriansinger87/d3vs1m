using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Network
{
    public class PeerToPeerNetworkSimulator : ISimulatable
    {
        private NetworkArgs _args;

        // -- constructor

        public PeerToPeerNetworkSimulator()
        {

        }

        // -- methods

        public ISimulatable With(BaseArgs arguments)
        {
            _args = arguments as NetworkArgs;
            return this;
        }

        public void Execute()
        {
            // TODO: run network simulation here...
        }

        // -- properties       

        public BaseArgs Arguments { get { return _args; } }

        public string Name { get { return _args.Name; } }

        public SimulationModels Model { get { return SimulationModels.Network; } }
    }
}
