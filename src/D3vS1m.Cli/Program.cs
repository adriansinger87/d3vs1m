using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene.Materials;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
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
                // -- init

                // TODO: adjust log level for cli app
                Log.Inject(new NLogger { MinRule = LogLevel.Debug, Suffix = "-suffix" }.Start());

                _runtime = new RuntimeController(new D3vS1mValidator());
                _factory = new D3vS1mFactory();

                SimArgs.Add(Models.Scene.Key, _factory.NewArgument(Models.Scene.Name));
                SimArgs.Add(Models.Communication.LrWpan.Key, _factory.NewArgument(Models.Communication.LrWpan.Name));
                ReadArgs(args);

                // -- setup

                _factory.SetupSimulation(_simArgs.Values.ToArray(), _runtime);

                // -- run

                if (_runtime.Validate() == false)
                {
                    throw new RuntimeException("The runtime validation failed.");
                }
                await _runtime.RunAsync(1);

                // -- Finished

                Console.WriteLine("Simulation successfull!");
            }
            catch(Exception ex)
            {
                Log.Fatal(ex);
            }
            finally
            {
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        // -- methods

        private static void ReadArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i += 2)
            {
                var key = feedKey(args[i]);
                var val = args[i + 1];
                feedArg(key, val, IO);
            }

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
                    case Models.Network.Key:
                        SimArgs.Add(key,
                            importer.Import().As<NetworkArgs>());
                        break;
                    case Models.Devices.Key:
                        var netArgs = new NetworkArgs();
                        var devices = importer.Import().As<List<BasicDevice>>();
                        netArgs.Network.AddRange(devices.ToArray());
                        SimArgs.Add(key, netArgs);
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