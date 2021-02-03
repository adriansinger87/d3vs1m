using System.Collections.Generic;
using D3vS1m.Application;
using D3vS1m.Application.Runtime.Options;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Persistence.Imports.Reader;

namespace D3vS1m.Persistence.Imports
{
	public class ImportPipeline
	{
		// --fields

		//private PersistenceController _io;

		// -- constructor

		public ImportPipeline(OptionsBase options, FactoryBase factory, RuntimeBase runtime)
		{
			Arguments = new Dictionary<SimulationTypes, ArgumentsBase>();
			Options = options;
			Factory = factory;
			Runtime = runtime;
		}

		// -- methods;

		public ImportPipeline Run(IReadable reader)
		{
			reader.Read(this);
			return this;
		}

		// -- properties

		public Dictionary<SimulationTypes, ArgumentsBase> Arguments { get; private set; }

		public OptionsBase Options { get; private set; }

		public FactoryBase Factory { get; private set; }

		public RuntimeBase Runtime { get; private set; }

		//public PersistenceController IO
		//{
		//	get
		//	{
		//		if (_io == null)
		//		{
		//			_io = new PersistenceController();
		//			_io.Add(D3vS1m.Persistence.Constants.Wavefront.Key, new ObjImporter());
		//		}
		//		return _io;
		//	}
		//}

	}
}
