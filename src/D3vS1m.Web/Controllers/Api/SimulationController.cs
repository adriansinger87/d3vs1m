using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Infrastructure.Mqtt;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using D3vS1m.Domain.System.Extensions;
using D3vS1m.Application.Channel;
using D3vS1m.Web.Extensions;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Exceptions;
using Sin.Net.Domain.Logging;

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

        private RuntimeBase _runtime;
        private IMqttControlable _mqtt;
        private string _consoleTopic;
        private string _disconnectTopic;

        // -- constructor

        public SimulationController(IHostingEnvironment env, FactoryBase factory, IMqttControlable mqtt) : base(env, factory)
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

        /// <summary>
        /// GET: api/simulation/run
        /// </summary>
        /// <returns></returns>
        [HttpGet("run/{guid}")]
        public async Task<JsonResult> Run(string guid)
        {
            try
            {
                if (_factory.Runtime.Arguments.Guid != guid)
                {
                    throw new RuntimeException($"Runtime guid '{_factory.Runtime.Arguments.Guid}' does not match to the client guid '{guid}'");
                }
                BuildTopics(guid);
                SetupSimulation();
                RunSimulationAsync();
            }
            catch(Exception ex)
            {
                await _mqtt.PublishAsync(_consoleTopic, BuildMessage(DateTime.Now, $"The Simulation had an exception: {ex.Message}"), 2);
            }

            return new JsonResult(_runtime.Arguments);
        }

        // -- private methods

 
      

        private void SetupSimulation()
        {
            // fetch the array of arguments for each simulator 
            var args = SessionArguments();

            // setup the simulators and attach them to the runtime, based on the existent args
            _runtime = _factory.SetupSimulation(args);

            _runtime.Started += OnStarted;
            _runtime.Stopped += OnStopped;
            _runtime.IterationPassed += OnIterationPassed;
            _runtime.Simulators.Items.ForEach(s => {
                s.Executed += OnSimulatorExecuted;
            });
        }

        private void RunSimulationAsync()
        {
            if (_runtime.Validate() == false)
            {
                throw new Exception("The validation of the simulation failed");
            }

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
            PublishConsoleTopic("### Simulation started");
        }

        private void OnStopped(object sender, SimulatorEventArgs e)
        {
            CleanupSimulation();

            // TODO @ AS: dont manipulate arguments during simulation this data should be moved and persisted into results data container
            //this.HttpSession().SetArguments(_factory.SimulationArguments);
            PublishConsoleTopic("### Simulation stopped");
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
            PublishConsoleTopic($"Iteration {runArgs.Iterations} done.<br />");
        }

        // -- helper

        private void PublishConsoleTopic(string message)
        {
            _mqtt.PublishAsync(_consoleTopic, BuildMessage(DateTime.Now, message), 2);
        } 

        private void BuildTopics(string guid)
        {
            _consoleTopic = $"{BASE_TOPIC}/{CONSOLE_TOPIC}";         // $"{BASE_TOPIC}/{guid}/{CONSOLE_TOPIC}"
            _disconnectTopic = $"{BASE_TOPIC}/{DISCONNECT_TOPIC}";   //  $"{BASE_TOPIC}/{guid}/{DISCONNECT_TOPIC}"
        }

        private string BuildMessage(DateTime timestamp, string message = null)
        {
            return $"{timestamp}{(string.IsNullOrEmpty(message) ? "" : $" - {message}")}";
        }
    }
}
