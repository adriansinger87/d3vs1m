using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Data;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Scene;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeleScope.Logging;
using TeleScope.Logging.Extensions.Serilog;
using TeleScope.Persistence.Abstractions;
using TeleScope.Persistence.Csv;
using TeleScope.Persistence.Json;

namespace MSTests
{
	public abstract class TestBase
	{
		// -- fields

		protected ILogger _log;
		protected const string APP_LOCATION = "App_Data";

		// -- basic methods


		[TestInitialize]
		public virtual void Arrange()
		{
			LoggingProvider.Initialize(
				 new LoggerFactory()
					 .UseTemplate("{Timestamp: HH:mm:ss} [{Level} | {SourceContext:l}] - {Message}{NewLine}{Exception}")
					 .UseLevel(LogLevel.Trace)
					 .AddSerilogConsole());
			_log = LoggingProvider.CreateLogger<TestsBase>();
		}

		[TestCleanup]
		public virtual void Cleanup()
		{

		}

		public void LoadAntennaData(SphericAntennaArgs args, string file = "PCB_868_tot.csv")
		{
			var setup = new CsvStorageSetup(new FileInfo(Path.Combine(APP_LOCATION, file)), 1);
			var parser = new CsvToRowParser(24);
			var csv = new CsvStorage<DataRow>(setup, parser, new GainMatrixToCsvParser());
			csv.Read();
			var table = parser.Table;

			args.GainMatrix = new TableToAntennaGainAdapter().Adapt(table);
		}

		public RuntimeController GetRuntime()
		{
			var runtime = new RuntimeController(new D3vS1mValidator());

			return runtime;
		}

		public AdaptedFriisArgs GetRadioArgs()
		{
			var min = new Vertex(-10, -10, -10);
			var max = new Vertex(10, 10, 10);
			var radioArgs = new AdaptedFriisArgs();

			radioArgs.RadioBox.Resolution = 0.25F;
			radioArgs.RadioBox.MinCorner = min;
			radioArgs.RadioBox.MaxCorner = max;

			WriteJson(radioArgs, "adapted_friis_Args.json");
			// update the positions always when the box changes
			radioArgs.RxPositions = radioArgs.RadioBox.CreateRxPositions();

			return radioArgs;
		}

		public IEnumerable<SimpleDevice> ImportDevices()
		{
			return ImportDevices(Path.Combine(APP_LOCATION, "devices.json"));
		}

		public IEnumerable<SimpleDevice> ImportDevices(string file)
		{
			return new JsonStorage<SimpleDevice>(file).Read();
		}

		public void WriteJson(object obj, string file)
		{
			new JsonStorage<object>(file).Write(new object[] { obj });
		}


	}

	class CsvToRowParser : IParsable<DataRow>
	{
		private readonly DataTable _table;

		public DataTable Table => _table;

		public CsvToRowParser(int numberOfColumns)
		{
			var cols = Enumerable
				.Range(0, numberOfColumns)
				.Select(i => new DataColumn(GetAzimuth(i, numberOfColumns))).ToArray();
			_table = new DataTable();
			_table.Columns.AddRange(cols);
		}

		public DataRow Parse<Tin>(Tin input, int index = 0, int length = 1)
		{
			var fields = input as string[];
			var row = _table.NewRow();
			row.ItemArray = fields;
			_table.Rows.Add(row);
			return row;
		}

		private string GetAzimuth(int index, int count)
		{
			return (360 / count * index).ToString();
		}


	}

	class GainMatrixToCsvParser : IParsable<string[]>
	{
		public string[] Parse<Tin>(Tin input, int index = 0, int length = 1)
		{
			throw new NotImplementedException();
		}
	}

	class CsvToGainMatrixParser : IParsable<Matrix<SphericGain>>
	{
		public Matrix<SphericGain> Parse<Tin>(Tin input, int index = 0, int length = 1)
		{
			var fields = input as string[];
			var cols = fields.Length;
			var rows = length;
			var nAz = getAzimuthNumber(cols);
			var nEl = getElevationNumber(rows);

			var gainMatrix = new Matrix<SphericGain>(nEl, nAz);

			//gainMatrix.

			return new Matrix<SphericGain>();
		}

		// -- helper

		private int getAzimuthNumber(int cols)
		{
			return (cols % 2 == 0 ? cols : cols - 1);
		}

		private int getElevationNumber(int rows)
		{
			return (rows % 2 != 0 ? rows : rows - 1);
		}
	}
}
