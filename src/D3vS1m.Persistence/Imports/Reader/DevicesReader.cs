using D3vS1m.Application;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Persistence.Imports.Reader
{
    public class DevicesReader : IReadable
    {
        public void Read(ImportPipeline pipe)
        {
            var args = pipe.Factory.NewArgument(Models.Network.Name) as NetworkArgs;

            if (string.IsNullOrEmpty(pipe.Options.DevicesFile))
            {
                pipe.Arguments.Add(SimulationTypes.Network, args);
                return;
            }

            var setting = new JsonSetting
            {
                Location = pipe.Options.Workspace,
                Name = pipe.Options.DevicesFile
            };

            var devices = pipe.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Import()
                .As<List<BasicDevice>>();

            args.Network.AddRange(devices.ToArray());
            pipe.Arguments.Add(SimulationTypes.Network, args);
        }
    }
}
