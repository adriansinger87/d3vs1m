using System.IO;
using System.Linq;
using D3vS1m.Application;
using D3vS1m.Application.Channel;
using D3vS1m.Domain.System.Enumerations;
using TeleScope.Persistence.Json;

namespace D3vS1m.Infrastructure.Imports
{
	public class ChannelImporter : IImportable
	{
		public void Import(ImportPipeline pipe)
		{
			var args = pipe.Factory.NewArgument(Models.Channel.AdaptedFriis.Name) as AdaptedFriisArgs;

			if (!string.IsNullOrEmpty(pipe.Options.ChannelFile))
			{
				var file = Path.Combine(pipe.Options.Workspace, pipe.Options.ChannelFile);
				args = new JsonStorage<AdaptedFriisArgs>(file).Read().First();
			}

			pipe.Arguments.Add(SimulationTypes.Channel, args);
		}
	}
}
