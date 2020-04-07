using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using System;

namespace D3vS1m.Application.Communication
{
    /// <summary>
    /// Simulates important parts of the communication of Low Range Wireless Personal Area Networks (LW-WPAN)
    /// Implements the Functionality of the IEEE 802.15.4 standard
    /// </summary>
    [Serializable]
    public class LRWPANSimulator : SimulatorBase
    {
        // -- fields

        private WirelessCommArgs _commArgs;

        // -- constructor

        public LRWPANSimulator(RuntimeBase runtime) : base(runtime)
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _commArgs)) return this;
            else return ArgsNotAdded(arguments.Name);
        }

        public override void Run()
        {
            base.BeforeExecution();

            base.AfterExecution();
        }

        // -- properties

        public override string Id => Models.Communication.LrWpan.Name;
        public override string Name => Models.Communication.LrWpan.Name;
        public override SimulationModels Type => SimulationModels.Communication;
        public override ArgumentsBase Arguments => _commArgs;

    }
}
