using D3vS1m.Application.Runtime;

namespace D3vS1m.Persistence.Imports.Reader
{
	public class RuntimeReader : IReadable
	{
		public void Read(ImportPipeline pipe)
		{
			//if (string.IsNullOrEmpty(pipe.Options.RuntimeFile) == false)
			//{
			//	var setting = new JsonSetting
			//	{
			//		Location = pipe.Options.Workspace,
			//		Name = pipe.Options.RuntimeFile
			//	};

			//	var args = pipe.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
			//		.Setup(setting)
			//		.Import()
			//		.As<RuntimeArgs>();

			//	pipe.Runtime.With(args);
			//}
		}
	}
}
