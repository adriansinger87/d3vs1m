﻿using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Cli.Options;
using D3vS1m.Cli.Reader;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Exceptions;
using NLog;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D3vS1m.Cli
{
    internal class Program
    {

        // -- fields

        private static D3vS1mFactory _factory;
        private static RuntimeController _runtime;
        private static Dictionary<SimulationTypes, ArgumentsBase> _simArgs;

        // -- main

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Starting D3vS1m command line tool...");

            try
            {
                var options = new OptionParser().ReadArguments(args);

                var logger = new NLogger { MinRule = options.Verbose ? LogLevel.Trace : LogLevel.Info };
                Log.Inject(logger.Start());

                // -- read arguments

                ReadArgs(options);

                // -- setup simulation

                Factory.SetupSimulation(SimArgs.Values.ToArray(), Runtime);

                Runtime.Simulators[SimulationTypes.Antenna].With(SimArgs[SimulationTypes.Network]);
                Runtime.Stopped += (o, e) =>
                {
                    WaitForExit();
                };

                // -- run

                if (Runtime.Validate() == false)
                {
                    throw new RuntimeException("The runtime validation failed.");
                }

                await Runtime.RunAsync(5);

            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                WaitForExit();
            }
            finally
            {
            }
        }

        // -- methods

        private static void WaitForExit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void ReadArgs(CliOptions options)
        {
            Log.Info("Processing cli options");

            SimArgs = new ArgumentsReader(options, Factory, Runtime)
                .Run(new RuntimeReader())
                .Run(new EnergyReader())
                .Run(new DevicesReader())
                .Run(new SceneReader())
                .Run(new ChannelReader())
                .Run(new AntennaReader())
                .Run(new CommunicationReader())
                .Arguments;

            Log.Info("Cli options processed");
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