using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Logging;

namespace D3vS1m.Application.Antenna
{
    public class SimpleAntennaSimulator : SimulatorBase
    {
        // -- fields

        SimpleAntennaArgs _args;

        // -- constructor

        public SimpleAntennaSimulator()
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _args)) return this;
            else                                   return ArgsNotAdded(arguments.Name);

        }

        public override void Run()
        {
            base.BeforeExecution();

            base.AfterExecution();
        }

        // -- properties

        public override string Name { get { return _args.Name; } }

        public override SimulationModels Model { get { return SimulationModels.Antenna; } }

        public override ArgumentsBase Arguments { get { return _args; } }

    }
}
