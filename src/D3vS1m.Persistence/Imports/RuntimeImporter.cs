using System.IO;
using System.Linq;
using D3vS1m.Application.Runtime;
using TeleScope.Persistence.Json;

namespace D3vS1m.Persistence.Imports
{
	public class RuntimeImporter : IImportable
	{
		public void Import(ImportPipeline pipe)
		{
			if (!string.IsNullOrEmpty(pipe.Options.RuntimeFile))
			{
				var file = Path.Combine(pipe.Options.Workspace, pipe.Options.RuntimeFile);
				var args = new JsonStorage<RuntimeArgs>(file).Read().First();
				pipe.Runtime.With(args);
			}
		}
	}
}
