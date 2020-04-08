using D3vS1m.Application;
using D3vS1m.Application.Channel;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Cli.Reader
{
    internal class ChannelReader : IReadable
    {
        public void Read(ArgumentsReader reader)
        {
            var args = reader.Factory.NewArgument(Models.Channel.AdaptedFriis.Name) as AdaptedFriisArgs;

            if (string.IsNullOrEmpty(reader.Options.ChannelFile))
            {
                reader.Arguments.Add(SimulationTypes.Channel, args);
                return;
            }

            var setting = new JsonSetting
            {
                Location = reader.Options.Workspace,
                Name = reader.Options.ChannelFile
            };

            args = reader.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Import()
                .As<AdaptedFriisArgs>();

            reader.Arguments.Add(SimulationTypes.Channel, args);
        }
    }
}
