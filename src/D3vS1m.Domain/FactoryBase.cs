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

        public abstract ArgumentsBase GetArgument(string name);
        public abstract ArgumentsBase[] GetPredefinedArguemnts();

        public abstract ISimulatable Register(ISimulatable simulator);
        public abstract ISimulatable Register(ISimulatable simulator, ArgumentsBase args);
        public abstract ISimulatable Register(ISimulatable simulator, ArgumentsBase[] argsArray);
        public abstract void RegisterPredefined();

        // -- properties

        public abstract ArgumentsBase[] Arguments { get; }
        public SimulatorRepository Simulators { get; protected set; }
    }
}