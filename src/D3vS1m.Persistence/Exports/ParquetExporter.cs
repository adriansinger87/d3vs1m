using Parquet;
using Parquet.Data;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Settings;
using System;
using System.IO;

namespace D3vS1m.Persistence.Exports
{
    public class ParquetExporter : IExportable
    {

        // -- fields

        private FileSetting _setting;

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
            throw new NotImplementedException();
        }

        public IExportable Build<T>(T data, IAdaptable adapter)
        {
            throw new NotImplementedException();
        }

        public string Export()
        {
            var idColumn = new DataColumn(
            new DataField<int>("id"),
            new int[] { 1, 2 });

            var cityColumn = new DataColumn(
               new DataField<string>("city"),
               new string[] { "London", "Derby" });

            // create file schema
            var schema = new Schema(idColumn.Field, cityColumn.Field);

            using (Stream fileStream = System.IO.File.OpenWrite(_setting.FullPath))
            {
                using (var parquetWriter = new ParquetWriter(schema, fileStream))
                {
                    // create a new row group in the file
                    using (ParquetRowGroupWriter groupWriter = parquetWriter.CreateRowGroup())
                    {
                        groupWriter.WriteColumn(idColumn);
                        groupWriter.WriteColumn(cityColumn);
                    }
                }
            }

            return _setting.FullPath;
        }



        // -- properties
        public string Type => D3vS1m.Persistence.Constants.Parquet.Key;
    }
}
