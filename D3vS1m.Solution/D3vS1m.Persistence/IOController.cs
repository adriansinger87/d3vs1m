using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence.Imports;
using Sin.Net.Domain.IO;
using Sin.Net.Persistence;

namespace D3vS1m.Persistence
{
    public class IOController : PersistenceController
    {
        // -- constructors

        public IOController() : base()
        {

        }

        // -- methods

        public override void InitExports()
        {
            base.InitExports();

            // add new export types here
        }

        public override void InitImports()
        {
            base.InitImports();
            IImportable import;

            import = new ObjImporter();
            _imports.Add(import.Type, import);

            // add new import types here
        }
    }
}
