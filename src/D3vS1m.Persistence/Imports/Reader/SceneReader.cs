using D3vS1m.Application;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Persistence.Imports.Reader
{
	public class SceneReader : IReadable
	{
		public void Read(ImportPipeline pipe)
		{
			var args = pipe.Factory.NewArgument(Models.Scene.Name);
			pipe.Arguments.Add(SimulationTypes.Scene, args);
		}
	}
}
