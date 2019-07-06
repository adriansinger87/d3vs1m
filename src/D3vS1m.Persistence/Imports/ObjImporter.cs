using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using Sin.Net.Persistence.Settings;
using System;
using System.IO;

namespace D3vS1m.Persistence.Imports
{
    internal class ObjImporter : IImportable
    {
        // -- fields

        private FileSetting _setting;
        private string _importObj;

        // -- methods

        public IImportable Setup(SettingsBase setting)
        {
            if (setting is FileSetting)
            {
                _setting = setting as FileSetting;
            }
            else
            {
                Log.Error("The json import setting has the wrong type and was not accepted.");
            }
            return this;
        }

        public IImportable Import()
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
        public T As<T>() where T : new()
        {
            return (T)Convert.ChangeType(_importObj, typeof(T));
        }

        public T As<T>(IAdaptable adapter) where T : new()
        {
            return adapter.Adapt<string, T>(_importObj);
        }

        public object AsItIs()
        {
            return _importObj;
        }

        // -- properties

        string IImportable.Type => D3vS1m.Persistence.Constants.Wavefront.Key;
    }
}
