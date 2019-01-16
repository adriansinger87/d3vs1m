﻿using D3vS1m.Application.Data;
using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace D3vS1m.Persistence.Settings
{
    public class SphericAntennaCasting : ICasteable
    {
        DataTable _csvTable;

        public Tout CastTo<Tout, Tin>(Tin input) where Tout : new()
        {
            // validation
            Type inType = typeof(Tin);
            Type outType = typeof(Tout);

            if (typeof(DataTable).IsAssignableFrom(inType) == false ||
                typeof(Matrix<SphericGain>).IsAssignableFrom(outType) == false)
            {
                Log.Error($"The casting from '{inType.Name}' to '{outType.Name}' is not supported by the {this.GetType().Name}.");
                return new Tout();
            }

            _csvTable = input as DataTable;

            // cast here...
            int nAz = getAzimuthNumber(_csvTable.Columns.Count);
            int nEl = getElevationNumber(_csvTable.Rows.Count);

            Matrix<SphericGain> gainMatrix = new Matrix<SphericGain>(nEl, nAz);

            gainMatrix.Each(initGain);
            gainMatrix.Each(readCsvField);

            return (Tout)Convert.ChangeType(gainMatrix, outType);
        }

        private int getAzimuthNumber(int cols)
        {
            return (cols % 2 == 0 ? cols : cols - 1);
        }

        private int getElevationNumber(int rows)
        {
            return (rows % 2 == 0 ? rows : rows - 1);
        }

        private SphericGain initGain(int row, int col, SphericGain gain)
        {
            return new SphericGain();
        }

        private SphericGain readCsvField(int row, int col, SphericGain gain)
        {
            gain.Azimuth = float.Parse(_csvTable.Columns[col].ColumnName);
            gain.Elevation = (180.0f / (float)(_csvTable.Rows.Count - 1)) * row;
            gain.Gain = float.Parse(_csvTable.Rows[row][col].ToString());
            return gain;
        }
    }
}
