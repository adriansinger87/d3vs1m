using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Events;

namespace D3vS1m.Application.Network
{
    public class PeerToPeerNetworkSimulator : SimulatorBase
    {
        private NetworkArgs _networkArgs;

        // -- constructor

        public PeerToPeerNetworkSimulator()
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _networkArgs))   return this;
            else                                            return ArgsNotAdded(arguments.Name);
        }

        public override void Run()
        {
            base.BeforeExecution();
            // TODO: run network simulation here...

            base.AfterExecution();
        }

        // -- properties       

        public override ArgumentsBase Arguments { get { return _networkArgs; } }

        public override string Name { get { return _networkArgs.Name; } }

        public override SimulationModels Model { get { return SimulationModels.Network; } }

    }
}
