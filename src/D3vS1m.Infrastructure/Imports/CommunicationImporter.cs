using D3vS1m.Application;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Infrastructure.Imports
{
	public class CommunicationImporter : IImportable
	{
		public void Import(ImportPipeline pipe)
		{
			var args = pipe.Factory.NewArgument(Models.Communication.LrWpan.Name);
			pipe.Arguments.Add(SimulationTypes.Communication, args);
		}
	}
}
