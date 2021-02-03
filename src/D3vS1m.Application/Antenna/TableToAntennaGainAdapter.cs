using D3vS1m.Application.Data;
using System;
using System.Data;

namespace D3vS1m.Application.Antenna
{
    public class TableToAntennaGainAdapter
    {
        // -- fields

        private DataTable _table;

        // -- methods

        public Matrix<SphericGain> Adapt(DataTable table)
        {
            _table = table;

            int nAz = getAzimuthNumber(_table.Columns.Count);
            int nEl = getElevationNumber(_table.Rows.Count);

            var gainMatrix = new Matrix<SphericGain>(nEl, nAz);

            gainMatrix.Each(initGain);
            gainMatrix.Each(readCsvField);

            return gainMatrix;
        }

        // -- helper methods

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
