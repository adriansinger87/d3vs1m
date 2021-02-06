using System.Collections.Generic;
using D3vS1m.Application;
using D3vS1m.Application.Runtime.Options;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Infrastructure.Imports
{
	public class ImportPipeline
	{
		// --fields

		// -- properties

		public Dictionary<SimulationTypes, ArgumentsBase> Arguments { get; private set; }

		public OptionsBase Options { get; private set; }

		public FactoryBase Factory { get; private set; }

		public RuntimeBase Runtime { get; private set; }

		// -- constructor

		public ImportPipeline(OptionsBase options, FactoryBase factory, RuntimeBase runtime)
		{
			Arguments = new Dictionary<SimulationTypes, ArgumentsBase>();
			Options = options;
			Factory = factory;
			Runtime = runtime;
		}

		// -- methods

		public ImportPipeline Run(IImportable importer)
		{
			importer.Import(this);
			return this;
		}
	}
}
