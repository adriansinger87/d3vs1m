using D3vS1m.Domain.Data.Arguments;
using Parquet;
using Parquet.Data;
using Parquet.Data.Rows;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace D3vS1m.Persistence.Exports
{
    public class ParquetExporter : IExportable
    {

        // -- fields

        private FileSetting _setting;

        private List<ArgumentsBase> _input;
        private List<DataColumn> _output;

        // -- constructors

        public ParquetExporter()
        {

        }

        // -- methods

        public IExportable Setup(SettingsBase setting)
        {
            if (setting is FileSetting)
            {
                _setting = setting as FileSetting;
            }
            else
            {
                Log.Error($"The {this.Type} import setting has the wrong type '{setting.GetType()}' and was not accepted.");
            }
            return this;
        }

        public IExportable Build<T>(T data)
        {
            _output = data as List<DataColumn>;
            return this;
        }

        public IExportable Build<T>(T data, IAdaptable adapter)
        {
            _input = data as List<ArgumentsBase>;
            _output = adapter.Adapt<List<ArgumentsBase>, List<DataColumn>>(_input);
            return this;
        }  

        public string Export()
        {
            // create file schema
            var schema = new Schema(_output.Select(c => c.Field).ToArray());

            using (Stream fileStream = System.IO.File.OpenWrite(_setting.FullPath))
            {
                using (var parquetWriter = new ParquetWriter(schema, fileStream))
                {
                    // create a new row group in the file
                    using (ParquetRowGroupWriter groupWriter = parquetWriter.CreateRowGroup())
                    {
                        foreach (var c in _output)
                        {
                            groupWriter.WriteColumn(c);
                        }
                    }
                }
            }

            return _setting.FullPath;
        }



        // -- properties
        public string Type => D3vS1m.Persistence.Constants.Parquet.Key;
    }
}
