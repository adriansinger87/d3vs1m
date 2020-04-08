using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Persistence.IO.Json;
using Sin.Net.Persistence.Settings;

namespace D3vS1m.Cli.Reader
{
    internal class AntennaReader : IReadable
    {
        private ArgumentsReader _reader;

        public AntennaReader()
        {

        }

        public void Read(ArgumentsReader reader)
        {
            _reader = reader;

            ArgumentsBase arg = reader.Factory.NewArgument(Models.Antenna.Simple.Name);

            if (string.IsNullOrEmpty(reader.Options.AntennaFile))
            {
                // no antenna option alias a file present
                reader.Arguments.Add(SimulationTypes.Antenna, arg);
                return; 
            }

            var setting = new JsonSetting
            {
                Location = reader.Options.Workspace,
                Name = reader.Options.AntennaFile
            };

            string json = reader.IO.Importer(Sin.Net.Persistence.Constants.Json.Key)
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
            reader.Arguments.Add(SimulationTypes.Antenna, arg);
        }

        private ArgumentsBase ReadSphericAntenna(string json)
        {
            var spa = JsonIO.FromJsonString<SphericAntennaArgs>(json);
            var csvSetting = new CsvSetting
            {
                Name = spa.DataSource
            };
            var csvKey = Sin.Net.Persistence.Constants.Csv.Key;
            spa.LoadData(_reader.IO, csvSetting, csvKey);

            return spa;
        }
    }
}
