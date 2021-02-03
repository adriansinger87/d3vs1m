namespace D3vS1m.Persistence.Imports.Reader
{
	public class ChannelReader : IReadable
	{
		public void Read(ImportPipeline pipe)
		{
			//var args = pipe.Factory.NewArgument(Models.Channel.AdaptedFriis.Name) as AdaptedFriisArgs;

			//if (string.IsNullOrEmpty(pipe.Options.ChannelFile))
			//{
			//    pipe.Arguments.Add(SimulationTypes.Channel, args);
			//    return;
			//}

			//var setting = new JsonSetting
			//{
			//    Location = pipe.Options.Workspace,
			//    Name = pipe.Options.ChannelFile
			//};

			//args = pipe.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
			//    .Setup(setting)
			//    .Import()
			//    .As<AdaptedFriisArgs>();

			//pipe.Arguments.Add(SimulationTypes.Channel, args);
		}
	}
}
