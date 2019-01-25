using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using D3vS1m.Domain.IO;
using D3vS1m.Persistence.Settings;
using D3vS1m.Domain.System.Logging;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Persistence.Imports
{
    /// <summary>
    /// Diese Klasse implementiert die IStrategyImportable Schnittstelle für den lesenden Zugriff auf CSV-Dateien
    /// </summary>
    class CsvImport : IImportable
    {
        // -- fields

        private DataTable _table;
        private CsvSettings _setting;

        // -- constructor

        public CsvImport()
        {
            _setting = new CsvSettings
            {
                Separator = ';',
                HasHeader = true
            };
        }

        // -- methods

        public IImportable Setup(IOSettingsBase setting)
        {
            if (setting is CsvSettings)
            {
                _setting = setting as CsvSettings;
            }
            else
            {
                Log.Error("The csv import setting has the wrong type and was not accepted.");
            }
            return this;
        }

        /// <summary>
        /// Importiert die CSV-Datei entsprechend der eingestellten Parameter und legt diese intern als DataTable-Objekt ab.
        /// </summary>
        /// <returns>Die aufrufende Objektinstanz für das Method-Chaining</returns>
        public IImportable Import()
        {
            var separator = _setting.Separator;
            var hasHeader = _setting.HasHeader;

            _table = new DataTable();
            _table.TableName = _setting.Name;

            string[] lines = File.ReadAllLines(_setting.FullPath);
            string[] fields = lines[0].Split(new char[] { separator });
            int length = fields.GetLength(0);

            //1st row can be used for column names
            for (int i = 0; i < length; i++)
            {
                if (hasHeader == true)
                {
                    _table.Columns.Add(fields[i].ToLower());
                }
                else
                {
                    _table.Columns.Add(i.ToString());
                }
            }

            // all other rows
            DataRow Row;
            int firstRowIndex = (hasHeader == true ? 1 : 0);
            for (int i = firstRowIndex; i < lines.Length; i++)
            {
                fields = lines[i].Split(new char[] { separator });
                Row = _table.NewRow();
                for (int f = 0; f < fields.Length; f++)
                {
                    Row[f] = fields[f];
                    
                }
                _table.Rows.Add(Row);
            }

            Log.Info($"csv import successfull for '{_setting.Name}'");
            return this;
        }

        public T CastTo<T>(ICasteable casting) where T: new()
        {
            return casting.CastTo<T, DataTable>(_table);
        }

        // -- properties

        public ImportTypes Type { get { return ImportTypes.Csv; } }
    }
}
