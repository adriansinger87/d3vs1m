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
				.Select(i => new DataColumn(GetAzimuthByIndex(i, numberOfColumns))).ToArray();
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
			if (_table == null || _table.Rows.Count == 0 || _table.Columns.Count == 0)
			{
				throw new FieldAccessException("The internal table has no data.");
			}
			int nAz = GetAzimuthNumber(_table.Columns.Count);
			int nEl = GetElevationNumber(_table.Rows.Count);

			var gainMatrix = new Matrix<SphericGain>(nEl, nAz);

			gainMatrix.Each(InitGain);
			gainMatrix.Each(ReadCsvField);

			return gainMatrix;
		}

		// -- helper methods

		private string GetAzimuthByIndex(int index, int count)
		{
			return (360 / count * index).ToString();
		}

		private int GetAzimuthNumber(int cols)
		{
			return (cols % 2 == 0 ? cols : cols - 1);
		}

		private int GetElevationNumber(int rows)
		{
			return (rows % 2 != 0 ? rows : rows - 1);
		}

		private SphericGain InitGain(int row, int col, SphericGain gain)
		{
			return new SphericGain();
		}

		private SphericGain ReadCsvField(int row, int col, SphericGain gain)
		{
			gain.Azimuth = float.Parse(_table.Columns[col].ColumnName);
			gain.Elevation = (180.0f / (float)(_table.Rows.Count - 1)) * row;
			gain.Gain = float.Parse(_table.Rows[row][col].ToString());
			return gain;
		}
	}
}
