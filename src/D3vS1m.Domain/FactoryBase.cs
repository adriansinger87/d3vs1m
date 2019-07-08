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

        public abstract ArgumentsBase[] GetPredefinedArguments();

        public abstract ISimulatable RegisterSimulator(ISimulatable simulator, ArgumentsBase args);

        public abstract RuntimeBase SetupSimulation(ArgumentsBase[] args);

        // -- properties

        public virtual RuntimeBase Runtime => _runtime;

        public virtual ArgumentsBase[] SimulationArguments => Simulators.AllArguments;

        protected SimulatorRepository Simulators { get; set; }


    }
}