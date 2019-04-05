using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence.Settings;
using Sin.Net.Domain.Logging;
using System.IO;

namespace D3vS1m.Persistence.Imports
{
    internal class ObjImport : IImportable
    {
        // -- fields

        private FileSettings _setting;
        private string _importObj;

        // -- methods

        public IImportable Setup(IOSettingsBase setting)
        {
            if (setting is FileSettings)
            {
                _setting = setting as FileSettings;
            }
            else
            {
                Log.Error("The json import setting has the wrong type and was not accepted.");
            }
            return this;
        }

        public T CastTo<T>(ICasteable casting) where T : new()
        {
            return casting.CastTo<T, string>(_importObj);
        }

        public IImportable Import()
        {
            StreamReader reader = new StreamReader(_setting.FullPath);
            _importObj = reader.ReadToEnd();
            return this;
        }

        // -- properties

        public ImportTypes Type { get { return ImportTypes.WavefrontObj; } }

    }
}
