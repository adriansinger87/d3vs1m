using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Simulation
{
    public interface ISimulatable
    {

        ISimulatable With(BaseArgs arguments);
        void Execute();

        // -- properties

        string Name { get; }

        SimulationModels Model { get; }
    }
}
