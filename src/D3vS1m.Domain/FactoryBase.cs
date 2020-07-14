using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;

namespace D3vS1m.Application
{
    public abstract class FactoryBase
    {
        // -- fields

        // -- constructors

        public FactoryBase()
        {
        }

        // -- methods

        public abstract ArgumentsBase NewArgument(string name);

        public abstract ISimulatable NewSimulator(string name, RuntimeBase runtime);

        public abstract ArgumentsBase[] GetPredefinedArguments();

        public abstract ISimulatable RegisterSimulator(ISimulatable simulator, ArgumentsBase args);

        public abstract RuntimeBase SetupRuntime(ArgumentsBase[] args, RuntimeBase runtime);

        // -- properties

        public virtual ArgumentsBase[] SimulationArguments => Simulators.AllArguments;

        protected SimulatorRepository Simulators { get; set; }


    }
}