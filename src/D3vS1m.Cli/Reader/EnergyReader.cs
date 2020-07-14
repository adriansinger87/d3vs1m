using D3vS1m.Application;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Cli.Reader
{
    internal class EnergyReader : IReadable
    {
        public void Read(ReaderPipeline reader)
        {
            var args = reader.Factory.NewArgument(Models.Energy.Battery.Name);
            args.Index = 999;
            reader.Arguments.Add(SimulationTypes.Energy, args);
        }
    }
}
