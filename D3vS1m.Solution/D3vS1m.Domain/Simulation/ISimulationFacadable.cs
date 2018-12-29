using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Simulation
{
    public interface ISimulationFacadable
    {
        void RegisterSimulation();

        void Register(ISimulatable simulator, ArgumentsBase args);

        SimulatorRepository SimulatorRepo { get; }
    }
}
