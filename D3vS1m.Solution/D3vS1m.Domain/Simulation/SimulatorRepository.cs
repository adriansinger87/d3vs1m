using D3vS1m.Domain.Data.Repositories;
using D3vS1m.Domain.Runtime;
using System;
using System.Collections.Generic;

namespace D3vS1m.Domain.Simulation
{
    public class SimulatorRepository : BaseRepository<ISimulatable>
    {
        public SimulatorRepository()
        {
            base.Name = "simulation models";
        }
    }
}
