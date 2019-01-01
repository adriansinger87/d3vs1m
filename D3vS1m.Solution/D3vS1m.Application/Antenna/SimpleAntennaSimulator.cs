using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;

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
            _args = arguments as SimpleAntennaArgs;

            return this;
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
