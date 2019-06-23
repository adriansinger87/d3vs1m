using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;

namespace D3vS1m.Application
{
    public abstract class FactoryBase
    {
        // -- fields

        protected RuntimeBase _runtime;
        
        // -- constructors

        public FactoryBase(RuntimeBase runtime)
        {
            _runtime = runtime;
        }

        // -- methods

        public abstract ArgumentsBase NewArgument(string name);

        public abstract ISimulatable NewSimulator(string name);

        public abstract ArgumentsBase[] GetPredefinedArguemnts();

        public abstract ISimulatable RegisterSimulator(ISimulatable simulator, ArgumentsBase args);

        public abstract SimulatorRepository CreateSimulation(ArgumentsBase[] args);

        // -- properties

        public abstract ArgumentsBase[] Arguments { get; }
        public SimulatorRepository Simulators { get; protected set; }
    }
}