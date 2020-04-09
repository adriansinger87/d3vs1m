using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Cli.Options;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence.Imports;
using Sin.Net.Persistence;
using Sin.Net.Persistence.IO.Json;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Cli.Reader
{
    public class ArgumentsReader
    {
        // --fields

        private PersistenceController _io;

        // -- constructor

        public ArgumentsReader(CliOptions options, FactoryBase factory, RuntimeBase runtime)
        {
            Arguments = new Dictionary<SimulationTypes, ArgumentsBase>();
            Options = options;
            Factory = factory;
            Runtime = runtime;
        }

        // -- methods;

        public ArgumentsReader Run(IReadable reader)
        {
            reader.Read(this);
            return this;
        }

        // -- properties

        public Dictionary<SimulationTypes, ArgumentsBase> Arguments { get; private set; }

        public CliOptions Options { get; private set; }

        public FactoryBase Factory { get; private set; }

        public RuntimeBase Runtime { get; private set; }

        public PersistenceController IO
        {
            get
            {
                if (_io == null)
                {
                    _io = new PersistenceController();
                    _io.Add(D3vS1m.Persistence.Constants.Wavefront.Key, new ObjImporter());
                }
                return _io;
            }
        }

    }
}
