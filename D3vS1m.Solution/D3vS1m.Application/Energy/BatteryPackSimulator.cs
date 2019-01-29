using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Energy
{
    public class BatteryPackSimulator : SimulatorBase
    {
        // -- fields

        private BatteryArgs _batteryArgs;

        // -- connstructor

        public BatteryPackSimulator()
        {
        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _batteryArgs))   return this;
            else                                            return ArgsNotAdded(arguments.Name);
        }

        public override void Run()
        {
            base.BeforeExecution();

            // TODO: run energy simulation here...

            base.AfterExecution();
        }
        
        // -- properties

        public override string Name { get { return _batteryArgs.Name; } }

        public override SimulationModels Model { get { return SimulationModels.Energy; } }

        public override ArgumentsBase Arguments { get { return _batteryArgs; } }
    }
}
