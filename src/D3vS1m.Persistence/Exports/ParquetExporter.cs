using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using D3vS1m.Domain.Data.Arguments;
using Parquet;
using Parquet.Data;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Exports;
using Sin.Net.Persistence.Settings;

namespace D3vS1m.Persistence.Exports
{
	public class ParquetExporter : ExporterBase
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

		public override IExportable Setup(SettingsBase setting)
		{
			try
			{
				_setting = setting as FileSetting;
			}
			catch (Exception ex)
			{
				Log.Error($"The {this.Type} import setting has the wrong type '{setting.GetType()}' and was not accepted.");
				base.HandleException(ex);
			}
			return this;
		}

		public override IExportable Build<T>(T data)
		{
			try
			{
				_output = data as List<DataColumn>;
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}

			return this;
		}

		public override IExportable Build<T>(T data, IAdaptable adapter)
		{
			try
			{
				_input = data as List<ArgumentsBase>;
				_output = adapter.Adapt<List<ArgumentsBase>, List<DataColumn>>(_input);
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
			
			return this;
		}

		public override string Export()
		{
			try
			{
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
			}
			catch (Exception ex)
			{
				base.HandleException(ex);
			}
		
			return _setting.FullPath;
		}
		
		// -- properties

		public override string Type => D3vS1m.Persistence.Constants.Parquet.Key;
	}
}
