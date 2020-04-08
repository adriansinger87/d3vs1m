using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene.Materials;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Exceptions;
using D3vS1m.Persistence.Imports;
using NLog;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Logging;
using Sin.Net.Persistence;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace D3vS1m.Cli
{
    internal class Program
    {

        // -- fields

        private static PersistenceController _io;
        private static D3vS1mFactory _factory;
        private static RuntimeController _runtime;
        private static Dictionary<string, ArgumentsBase> _simArgs;

        // -- main

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Starting D3vS1m command line tool...");
            try
            {
                Log.Inject(new NLogger { MinRule = LogLevel.Debug }.Start());

                // -- setup arguments

                SimArgs.Add(Models.Scene.Key, Factory.NewArgument(Models.Scene.Name));
                SimArgs.Add(Models.Communication.LrWpan.Key, Factory.NewArgument(Models.Communication.LrWpan.Name));
                ReadArgs(args);

                // -- setup simulation

                Factory.SetupSimulation(_simArgs.Values.ToArray(), Runtime);

                Runtime.Simulators[SimulationModels.Antenna].With(SimArgs[Models.Network.Key]);
                Runtime.Stopped += (o, e) =>
                {
                    WaitForExit();
                };

                // -- run

                if (Runtime.Validate() == false)
                {
                    throw new RuntimeException("The runtime validation failed.");
                }

                Runtime.RunAsync(2);

            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                WaitForExit();
            }
            finally
            {
                Console.ReadKey();
            }
        }

        // -- methods

        private static void WaitForExit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void ReadArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                var key = feedKey(args[i]);
                var val = args[i + 1];
                feedArg(key, val, IO);
            }

            Log.Trace("Reading cli arguments completed.");

            // -- local functions

            string feedKey(string arg)
            {
                return Regex.Replace(arg, @"[^0-9a-zA-Z]+", "").ToLower();
            }

            void feedArg(string key, string file, PersistenceController io)
            {
                Log.Debug($"Reading cli argument '{key}' with '{file}'.");

                // HACK: fixed magic string that all configs are in the subfolder "App_Data"
                var location = "App_Data";
                var setting = new JsonSetting
                {
                    Location = location,
                    Name = file
                };

                var importer = io.Importer(Constants.Json.Key).Setup(setting);

                // TODO: complete the reading of cli args
                switch (key)
                {
                    case Models.Runtime.Key:
                        var runArgs = importer.Import().As<RuntimeArgs>();
                        Runtime.SetArgruments(runArgs);
                        break;
                    case Models.Network.Key:
                        SimArgs.Add(key,
                            importer.Import().As<NetworkArgs>());
                        break;
                    case Models.Devices.Key:
                        var netArgs = new NetworkArgs();
                        var devices = importer.Import().As<List<BasicDevice>>();
                        netArgs.Network.AddRange(devices.ToArray());
                        SimArgs.Add(Models.Network.Key, netArgs);
                        break;
                    case Models.Antenna.Spheric.Key:
                        var antArgs = importer.Import().As<SphericAntennaArgs>();
                        var csvSettings = new CsvSetting
                        {
                            Location = location,
                            Name = antArgs.DataSource,
                        };
                        antArgs.LoadData(io, csvSettings, Constants.Csv.Key);
                        SimArgs.Add(key, antArgs);
                        break;
                    case Models.Channel.AdaptedFriis.Key:
                        SimArgs.Add(key,
                            importer.Import().As<AdaptedFriisArgs>());
                        break;
                    case Models.Scene.Materials.Key:
                        importer.Import().As<List<Material>>();
                        break;
                    default:
                        Log.Warn($"Cannot convert the key '{key}' into an arguemnts class.");
                        break;
                }
            }
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

        private static PersistenceController IO
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

        private static Dictionary<string, ArgumentsBase> SimArgs
        {
            get
            {
                if (_simArgs == null)
                {
                    _simArgs = new Dictionary<string, ArgumentsBase>();
                }
                return _simArgs;
            }
        }
    }
}