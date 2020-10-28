using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Persistence.IO.Json;
using Sin.Net.Persistence.Settings;

namespace D3vS1m.Persistence.Imports.Reader
{
    public class AntennaReader : IReadable
    {
        private ImportPipeline _pipe;

        public AntennaReader()
        {

        }

        public void Read(ImportPipeline pipe)
        {
            _pipe = pipe;

            ArgumentsBase arg = pipe.Factory.NewArgument(Models.Antenna.Simple.Name);

            if (string.IsNullOrEmpty(pipe.Options.AntennaFile))
            {
                // There is no antenna option or a file present
                pipe.Arguments.Add(SimulationTypes.Antenna, arg);
                return; 
            }

            var setting = new JsonSetting
            {
                Location = pipe.Options.Workspace,
                Name = pipe.Options.AntennaFile
            };

            string json = pipe.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Import()
                .AsItIs().ToString();

            var key = JsonIO.FromJsonString<EmptyArgs>(json).Key;

            switch (key)
            {
                case Models.Antenna.Simple.Key:
                    arg = JsonIO.FromJsonString<SimpleAntennaArgs>(json);
                    break;
                case Models.Antenna.Flat.Key:
                    arg = JsonIO.FromJsonString<FlatAntennaArgs>(json);
                    break;
                case Models.Antenna.Spheric.Key:               
                    arg = ReadSphericAntenna(json);
                    break;
                default:
                    break;
            }

            // arguments file loaded
            pipe.Arguments.Add(SimulationTypes.Antenna, arg);
        }

        private ArgumentsBase ReadSphericAntenna(string json)
        {
            var spa = JsonIO.FromJsonString<SphericAntennaArgs>(json);
            var csvSetting = new CsvSetting
            {
                Name = spa.DataSource
            };
            var csvKey = Sin.Net.Persistence.Constants.Csv.Key;
            spa.LoadData(_pipe.IO, csvSetting, csvKey);

            return spa;
        }
    }
}
