using D3vS1m.Domain.Data.Repositories;

namespace D3vS1m.Domain.Simulation
{
    public class SimulatorRepository : RepositoryBase<ISimulatable>
    {
        public SimulatorRepository()
        {
            base.Name = "simulation models";
        }
    }
}
