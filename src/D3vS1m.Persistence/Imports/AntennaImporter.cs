using System.Data;
using System.IO;
using System.Linq;
using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using TeleScope.Persistence.Csv;
using TeleScope.Persistence.Json;

namespace D3vS1m.Persistence.Imports
{
	public class AntennaImporter : IImportable
	{
		private ImportPipeline _pipe;

		public AntennaImporter()
		{

		}

		public void Import(ImportPipeline pipe)
		{
			_pipe = pipe;
			var arg = pipe.Factory.NewArgument(Models.Antenna.Simple.Name);

			if (!string.IsNullOrEmpty(pipe.Options.AntennaFile))
			{
				var file = Path.Combine(pipe.Options.Workspace, pipe.Options.AntennaFile);
				var key = ReadJson<EmptyArgs>(file).Key;

				switch (key)
				{
					case Models.Antenna.Simple.Key:
						arg = ReadJson<SimpleAntennaArgs>(file);
						break;
					case Models.Antenna.Flat.Key:
						arg = ReadJson<FlatAntennaArgs>(file);
						break;
					case Models.Antenna.Spheric.Key:
						arg = ReadSphericAntenna(file);
						break;
					default:
						break;
				}
			}

			// arguments file loaded
			pipe.Arguments.Add(SimulationTypes.Antenna, arg);
		}

		private T ReadJson<T>(string file)
		{
			return new JsonStorage<T>(file).Read().First();
		}

		private ArgumentsBase ReadSphericAntenna(string file)
		{
			var spa = ReadJson<SphericAntennaArgs>(file);

			// TODO: remove magic numer that is used to read csv table
			var columns = 24;
			var antennaDataFile = Path.Combine(_pipe.Options.Workspace, spa.DataSource);
			var setup = new CsvStorageSetup(new FileInfo(antennaDataFile), 1);
			var parser = new CsvToAntennaGainParser(columns);
			var csv = new CsvStorage<DataRow>(setup, parser, new AntennaGainToCsvParser());
			csv.Read();
			spa.GainMatrix = parser.GetGainMatrix();

			return spa;
		}
	}
}
