using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Communication
{
    /// <summary>
    /// Simulates important parts of the communication of Low Range Wireless Personal Area Networks (LW-WPAN)
    /// Implements the Functionality of the IEEE 802.15.4 standard
    /// </summary>
    public class LRWPANSimulator : SimulatorBase
    {
        // -- fields

        WirelessCommArgs _args;

        // -- constructor

        public LRWPANSimulator()
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (arguments is WirelessCommArgs) _args = arguments as WirelessCommArgs;
            else
            {
                Log.Warn($"'{arguments.Name}' arguments were not added to the '{this.GetType().Name}'");
            }
            return this;
        }

        public override void Run()
        {
            base.BeforeExecution();

            base.AfterExecution();
        }

        // -- properties

        public override string Name { get { return _args.Name; } }

        public override SimulationModels Model { get { return SimulationModels.Communication; } }

        public override ArgumentsBase Arguments { get { return _args; } }

    }
}
