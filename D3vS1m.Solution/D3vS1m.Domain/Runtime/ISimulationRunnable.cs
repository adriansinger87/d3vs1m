using D3vS1m.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Runtime
{
    public interface ISimulationRunnable
    {
        void Setup(RuntimeController control);

        void RunSimulation();

        // -- properties

        string Name { get; }

    }
}
