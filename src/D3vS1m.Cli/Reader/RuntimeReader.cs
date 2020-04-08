using D3vS1m.Application.Runtime;
using Sin.Net.Persistence.Settings;

namespace D3vS1m.Cli.Reader
{
    internal class RuntimeReader : IReadable
    {
        public void Read(ArgumentsReader reader)
        {
            if (string.IsNullOrEmpty(reader.Options.RuntimeFile) == false)
            {
                var setting = new JsonSetting
                {
                    Location = reader.Options.Workspace,
                    Name = reader.Options.RuntimeFile
                };

                var args = reader.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
                    .Setup(setting)
                    .Import()
                    .As<RuntimeArgs>();

                (reader.Runtime as RuntimeController).SetArgruments(args);
            }
        }
    }
}
