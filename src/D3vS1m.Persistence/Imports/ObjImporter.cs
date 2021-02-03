using System;
using System.IO;

namespace D3vS1m.Persistence.Imports
{
	public class ObjImporter
	{
		// -- fields
		/*
		private FileSetting _setting;
		private string _importObj;

		// -- methods

		public override IImportable Setup(SettingsBase setting)
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

		public override IImportable Import()
		{
			StreamReader reader = new StreamReader(_setting.FullPath);
			_importObj = reader.ReadToEnd();
			return this;
		}

		/// <summary>
		/// Returns the text content of the imported Wavefront (.obj) file as type T.
		/// </summary>
		/// <typeparam name="T">T should be a string</typeparam>
		/// <returns>The string of the stream data converted to string</returns>
		public override T As<T>()
		{
			return (T)Convert.ChangeType(_importObj, typeof(T));
		}

		public override T As<T>(IAdaptable adapter)
		{
			return adapter.Adapt<string, T>(_importObj);
		}

		public override object AsItIs()
		{
			return _importObj;
		}

		// -- properties

		public override string Type => D3vS1m.Persistence.Constants.Wavefront.Key;

		*/
	}
}
