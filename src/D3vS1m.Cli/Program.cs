using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Cli.Options;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Exceptions;
using D3vS1m.Infrastructure.Imports;
using Microsoft.Extensions.Logging;
using TeleScope.Logging;
using TeleScope.Logging.Extensions;
using TeleScope.Logging.Extensions.Serilog;
using TeleScope.UI.Cli.Options;

namespace D3vS1m.Cli
{
	public static class Program
	{
		// -- fields
	
		private static ILogger _log;
		private static D3vS1mFactory _factory;
		private static RuntimeController _runtime;
		private static Dictionary<SimulationTypes, ArgumentsBase> _simArgs;

		// -- main

		public static async Task Main(string[] args)
		{
			var options = new CliOptionParser<CliOptions> { Prefix = "--" }.ReadArguments(args);
			LoggingProvider.Initialize(
				 new LoggerFactory()
					 .UseLevel(options.GetLogLevel())
					 .AddSerilogConsole());
			_log = LoggingProvider.CreateLogger(typeof(Program));
			_log.Info($"Starting D3vS1m command line tool...");
			_log.Debug($"Start arguments: {string.Join(' ', args)}");

			try
			{
				ImportSimulationArguments(options);
				Factory.SetupRuntime(SimArgs.Values.ToArray(), Runtime);

				Runtime.SetupSimulators((repo) =>
				{
					repo[SimulationTypes.Antenna].With(SimArgs[SimulationTypes.Network]);
					repo[SimulationTypes.Energy].With(SimArgs[SimulationTypes.Network]);
				});

				Runtime.Stopped += (o, e) =>
				{
					WaitAndExit(options.Break);
				};

				Runtime.IterationPassed += (o, e) =>
				{

				};

				// -- validate

				if (!Runtime.Validate())
				{
					throw new RuntimeException("The runtime validation failed.");
				}

				// -- run

				await Runtime.RunAsync(5);

			}
			catch (Exception ex)
			{
				_log.Critical(ex);
				WaitAndExit(options.Break);
			}
		}

		// -- methods

		private static void WaitAndExit(bool wait = false)
		{
			if (wait)
			{
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}
			else
			{
				Console.WriteLine("Goodby!");
			}
		}

		private static void ImportSimulationArguments(CliOptions options)
		{
			_log.Debug("Processing cli options");

			SimArgs = new ImportPipeline(options, Factory, Runtime)
				.Run(new RuntimeImporter())
				.Run(new EnergyImporter())
				.Run(new DevicesImporter())
				.Run(new SceneImporter())
				.Run(new ChannelImporter())
				.Run(new AntennaImporter())
				.Run(new CommunicationImporter())
				.Arguments;

			_log.Debug("Cli options processed");
		}

		// -- properties

		private static D3vS1mFactory Factory
		{
			get
			{
				if (_factory == null)
				{
					_factory = new D3vS1mFactory();
				}
				return _factory;
			}
		}

		private static RuntimeController Runtime
		{
			get
			{
				if (_runtime == null)
				{
					_runtime = new RuntimeController(new D3vS1mValidator());
				}
				return _runtime;
			}
		}

		private static Dictionary<SimulationTypes, ArgumentsBase> SimArgs
		{
			get
			{
				if (_simArgs == null)
				{
					_simArgs = new Dictionary<SimulationTypes, ArgumentsBase>();
				}
				return _simArgs;
			}
			set
			{
				_simArgs = value;
			}
		}
	}
}