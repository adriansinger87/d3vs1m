using D3vS1m.Application;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Persistence.Imports
{
    public class EnergyImporter : IImportable
    {
        public void Import(ImportPipeline pipe)
        {
            var args = pipe.Factory.NewArgument(Models.Energy.Battery.Name);
            args.Index = 999;
            pipe.Arguments.Add(SimulationTypes.Energy, args);
        }
    }
}
