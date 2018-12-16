using D3vS1m.Domain.Data.Repositories;
using D3vS1m.Domain.Runtime;

namespace D3vS1m.Domain.Simulation
{
    public class SimulationRunnerRepository : BaseRepository<ISimulationRunnable>
    {
        public SimulationRunnerRepository()
        {
            base.Name = "runner for simulation models";
        }
    }
}
