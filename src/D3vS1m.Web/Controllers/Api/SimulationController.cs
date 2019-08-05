using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application;
using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Energy;
using D3vS1m.Application.Network;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Infrastructure.Mqtt;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Exceptions;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Sin.Net.Domain.Persistence.Logging;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ApiControllerBase
    {
        // -- fields

        private const string BASE_TOPIC = "d3vs1m";
        private const string CONSOLE_TOPIC = "console";
        private const string DISCONNECT_TOPIC = "disconnect";

        private const int ITERATIONS = 10;
        private string _consoleTopic;
        private string _disconnectTopic;
        private readonly IMqttControlable _mqtt;

        private RuntimeBase _runtime;

        // -- constructor

        public SimulationController(IHostingEnvironment env, FactoryBase factory, IMqttControlable mqtt) : base(env,
            factory)
        {
            _mqtt = mqtt;
        }

        // -- methods

        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            (_factory.Runtime.Arguments as RuntimeArgs).Reset();
            return new JsonResult(_factory.Runtime.Arguments);
        }

        [HttpPost("test")]
        public JsonResult PostTest([FromBody] JObject[] name)
        {
            return new JsonResult("OK");
        }


        /// <summary>
        ///     GET: api/simulation/run
        /// </summary>
        /// <returns></returns>
        [HttpPost("run/{guid}")]
        public async Task<JsonResult> Run(string guid, [FromBody] JObject[] args)
        {
            try
            {
                if (_factory.Runtime.Arguments.Guid != guid)
                    throw new RuntimeException(
                        $"Runtime guid '{_factory.Runtime.Arguments.Guid}' does not match to the client guid '{guid}'");
                BuildTopics(guid);

//                List<ArgumentsBase> argumentsList = new List<ArgumentsBase>();
/*
                foreach (var argJobject in args)
                {
                    var obj = JsonConvert.DeserializeObject<ArgumentsBase>(argJobject.ToString());
                    //argumentsList.Add(argJobject.ToObject<ArgumentsBase>());
                }
  */
                //  var arguments = JsonConvert.DeserializeObject<List<ArgumentsBase>>(args.ToString());


                var knownTypesBinder = new KnownTypesBinder
                {
                    KnownTypes = new List<Type>
                    {
                        typeof(AdaptedFriisArgs),
                        typeof(SimpleAntennaArgs),
                        typeof(FlatAntennaArgs),
                        typeof(SphericAntennaArgs),
                        typeof(WirelessCommArgs),
                        typeof(BatteryArgs),
                        typeof(NetworkArgs),
                        typeof(InvariantSceneArgs)
                    }
                };



                var objs = JsonConvert.DeserializeObject<ArgumentsBase>(args[3].ToString(),new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects,
                    SerializationBinder = knownTypesBinder
                });


                var argValStr = "";
                for (var i = 0; i < args.Length; i++)
                    if (i != args.Length - 1)
                        argValStr += args[i] + ",";
                    else
                        argValStr += args[i].ToString();

                var argVal = "[" + argValStr + "]";
                //var arguments = JsonIO.FromJsonString<ArgumentsBase>(args[3].ToString(), HttpSessionExtensions.ArgumentsBinder);
                return new JsonResult(objs);
                //SetupSimulation(arguments.ToArray());
                //RunSimulationAsync();
            }
            catch (Exception ex)
            {
                await _mqtt.PublishAsync(_consoleTopic,
                    BuildMessage(DateTime.Now, $"The Simulation had an exception: {ex.Message}"), 2);
            }

            return new JsonResult(_runtime.Arguments);
        }

        // -- private methods

        private void SetupSimulation(ArgumentsBase[] args)
        {
            // fetch the array of arguments for each simulator 
            //var args = SessionArguments();

            // setup the simulators and attach them to the runtime, based on the existent args
            _runtime = _factory.SetupSimulation(args);

            _runtime.Started += OnStarted;
            _runtime.Stopped += OnStopped;
            _runtime.IterationPassed += OnIterationPassed;
            _runtime.Simulators.Items.ForEach(s => { s.Executed += OnSimulatorExecuted; });
        }

        private void RunSimulationAsync()
        {
            if (_runtime.Validate() == false) throw new Exception("The validation of the simulation failed");

            // run until break condition
            var task = _runtime.RunAsync(1);
        }

        private void CleanupSimulation()
        {
            Log.Info("Cleaning runtime events");
            _factory.Runtime.Started -= OnStarted;
            _factory.Runtime.Stopped -= OnStopped;
            _factory.Runtime.IterationPassed -= OnIterationPassed;
        }

        // -- event methods

        private void OnStarted(object sender, SimulatorEventArgs e)
        {
            PublishConsoleTopic("### Simulation started<br />");
        }

        private void OnStopped(object sender, SimulatorEventArgs e)
        {
            CleanupSimulation();

            // TODO @ AS: dont manipulate arguments during simulation this data should be moved and persisted into results data container
            //this.HttpSession().SetArguments(_factory.SimulationArguments);
            PublishConsoleTopic("### Simulation stopped");
            _mqtt.PublishAsync(_disconnectTopic, BuildMessage(DateTime.Now), 2);
        }

        private void OnSimulatorExecuted(object sender, SimulatorEventArgs e)
        {
            PublishConsoleTopic($"{e.Arguments.Name} passed");
            if (e.Arguments is AdaptedFriisArgs)
            {
                var channelArgs = e.Arguments as AdaptedFriisArgs;
                PublishConsoleTopic($"{channelArgs.RadioBox.TotalData} data points calculated");
            }
        }

        private void OnIterationPassed(object sender, SimulatorEventArgs e)
        {
            var runArgs = e.Arguments as RuntimeArgs;
            PublishConsoleTopic($"Iteration {runArgs.Iterations} done<br />");
        }

        // -- helper

        private void PublishConsoleTopic(string message)
        {
            _mqtt.PublishAsync(_consoleTopic, BuildMessage(DateTime.Now, message), 2);
        }

        private void BuildTopics(string guid)
        {
            _consoleTopic = $"{BASE_TOPIC}/{guid}/{CONSOLE_TOPIC}";
            _disconnectTopic = $"{BASE_TOPIC}/{guid}/{DISCONNECT_TOPIC}";
        }

        private string BuildMessage(DateTime timestamp, string message = null)
        {
            return $"{timestamp}{(string.IsNullOrEmpty(message) ? "" : $" - {message}")}";
        }
    }

    public class KnownTypesBinder : ISerializationBinder
    {
        public IList<Type> KnownTypes { get; set; }

        public Type BindToType(string assemblyName, string typeName)
        {
            return KnownTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}