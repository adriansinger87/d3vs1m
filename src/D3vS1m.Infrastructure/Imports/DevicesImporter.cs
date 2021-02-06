using System.Collections.Generic;
using System.IO;
using System.Linq;
using D3vS1m.Application;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Domain.System.Enumerations;
using TeleScope.Persistence.Json;

namespace D3vS1m.Infrastructure.Imports
{
	public class DevicesImporter : IImportable
	{
		public void Import(ImportPipeline pipe)
		{
			var args = pipe.Factory.NewArgument(Models.Network.Name) as NetworkArgs;

			if (!string.IsNullOrEmpty(pipe.Options.DevicesFile))
			{
				var file = Path.Combine(pipe.Options.Workspace, pipe.Options.DevicesFile);
				var devices = new JsonStorage<SimpleDevice[]>(file).Read().First();
				args.Network.AddRange(devices);
			}

			pipe.Arguments.Add(SimulationTypes.Network, args);
		}
	}
}
