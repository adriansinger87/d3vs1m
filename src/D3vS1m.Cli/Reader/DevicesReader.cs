using D3vS1m.Application;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Cli.Reader
{
    internal class DevicesReader : IReadable
    {
        public void Read(ReaderPipeline reader)
        {
            var args = reader.Factory.NewArgument(Models.Network.Name) as NetworkArgs;

            if (string.IsNullOrEmpty(reader.Options.DevicesFile))
            {
                reader.Arguments.Add(SimulationTypes.Network, args);
                return;
            }

            var setting = new JsonSetting
            {
                Location = reader.Options.Workspace,
                Name = reader.Options.DevicesFile
            };

            var devices = reader.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Import()
                .As<List<BasicDevice>>();

            args.Network.AddRange(devices.ToArray());
            reader.Arguments.Add(SimulationTypes.Network, args);
        }
    }
}
