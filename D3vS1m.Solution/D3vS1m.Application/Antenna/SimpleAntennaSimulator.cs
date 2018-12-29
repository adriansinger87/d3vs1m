using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;

namespace D3vS1m.Application.Antenna
{
    public class SimpleAntennaSimulator : ISimulatable
    {
        // -- fields

        SimpleAntennaArgs _args;

        // -- constructor

        public SimpleAntennaSimulator()
        {

        }

        // -- methods

        public ISimulatable With(ArgumentsBase arguments)
        {
            _args = arguments as SimpleAntennaArgs;

            return this;
        }

        public void Execute()
        {
           
        }

        // -- properties

        public string Name { get { return _args.Name; } }

        public SimulationModels Model { get { return SimulationModels.Antenna; } }

        public ArgumentsBase Arguments { get { return _args; } }
    }
}
