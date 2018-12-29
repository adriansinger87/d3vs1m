using D3vS1m.Domain.IO;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence.Imports;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Persistence
{
    public class IOController : IOControllable
    {
        // -- fields

        private Dictionary<ExportTypes, IExportable> _exports;
        private Dictionary<ImportTypes, IImportable> _imports;

        // -- constructors

        public IOController()
        {
            InitExports();
            InitImports();
        }

        // -- methods

        public void InitExports()
        {
            _exports = new Dictionary<ExportTypes, IExportable>();

            // add new export types here
        }

        public void InitImports()
        {
            _imports = new Dictionary<ImportTypes, IImportable>();
            IImportable import;

            import = new JsonImport();
            _imports.Add(import.Type, import);


            // add new import types here
        }

        /// <summary>
        /// Gibt die Export-Funktionalität entsprechend des Typs aus
        /// </summary>
        /// <param name="type">Die Art des Exports</param>
        /// <returns>die Export-Funktionalität</returns>
        public IExportable Exporter(ExportTypes type)
        {
            return _exports[type];
        }

        /// <summary>
        /// Gibt die Import-Funktionalität entsprechend des Typs aus
        /// </summary>
        /// <param name="type">Die Art des Imports</param>
        /// <returns>die Import-Funktionalität</returns>
        public IImportable Importer(ImportTypes type)
        {
            return _imports[type];
        }
    }
}
