using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Logging;
using D3vS1m.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Persistence.Imports
{
    class JsonImport : IImportable
    {
        // -- fields

        private FileSettings _setting;

        string _importJson;

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
            return casting.CastTo<T, string>(_importJson);
        }

        public IImportable Import()
        {
            _importJson = JsonIO.LoadFromJson<string>(_setting.FullPath);
            return this;
        }

        // -- properties

        public ImportTypes Type { get { return ImportTypes.Json; } }
    }
}
