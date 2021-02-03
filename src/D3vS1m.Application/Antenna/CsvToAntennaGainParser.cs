using D3vS1m.Application.Data;
using System;
using System.Data;
using System.Linq;
using TeleScope.Persistence.Abstractions;

namespace D3vS1m.Application.Antenna
{
	public class CsvToAntennaGainParser : IParsable<DataRow>
	{

		// -- properties

		private readonly DataTable _table;

		// -- constructor

		public CsvToAntennaGainParser(int numberOfColumns)
		{
			var cols = Enumerable
				.Range(0, numberOfColumns)
				.Select(i => new DataColumn(GetAzimuth(i, numberOfColumns))).ToArray();
			_table = new DataTable();
			_table.Columns.AddRange(cols);
		}

		// -- methods

		public DataRow Parse<Tin>(Tin input, int index = 0, int length = 1)
		{
			var fields = input as string[];
			var row = _table.NewRow();
			row.ItemArray = fields;
			_table.Rows.Add(row);
			return row;
		}

		public Matrix<SphericGain> GetGainMatrix()
		{
			int nAz = getAzimuthNumber(_table.Columns.Count);
			int nEl = getElevationNumber(_table.Rows.Count);

			var gainMatrix = new Matrix<SphericGain>(nEl, nAz);

			gainMatrix.Each(initGain);
			gainMatrix.Each(readCsvField);

			return gainMatrix;
		}

		// -- helper methods

		private string GetAzimuth(int index, int count)
		{
			return (360 / count * index).ToString();
		}

		private int getAzimuthNumber(int cols)
		{
			return (cols % 2 == 0 ? cols : cols - 1);
		}

		private int getElevationNumber(int rows)
		{
			return (rows % 2 != 0 ? rows : rows - 1);
		}

		private SphericGain initGain(int row, int col, SphericGain gain)
		{
			return new SphericGain();
		}

		private SphericGain readCsvField(int row, int col, SphericGain gain)
		{
			gain.Azimuth = float.Parse(_table.Columns[col].ColumnName);
			gain.Elevation = (180.0f / (float)(_table.Rows.Count - 1)) * row;
			gain.Gain = float.Parse(_table.Rows[row][col].ToString());
			return gain;
		}
	}
}
