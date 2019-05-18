using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Simulation
{
    public interface ISimulationFacadable
    {
        void RegisterPredefined(RuntimeBase runtime);

        ISimulatable Register(ISimulatable simulator);

        ISimulatable Register(ISimulatable simulator, ArgumentsBase args);

        ISimulatable Register(ISimulatable simulator, ArgumentsBase[] argsArray);

        // -- properties

        SimulatorRepository Simulators { get; }

        Dictionary<string, ArgumentsBase[]> Arguments { get; }
    }
}
