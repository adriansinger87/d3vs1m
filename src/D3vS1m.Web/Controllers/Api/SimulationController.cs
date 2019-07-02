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

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ApiControllerBase
    {
        private const int ITERATIONS = 10;
        private const string CONSOLE_TOPIC = "d3vs1m/console";

        RuntimeBase _runtime;
        IMqttControlable _mqtt;

        public SimulationController(IHostingEnvironment env, FactoryBase factory, IMqttControlable mqtt) : base(env, factory)
        {
            _mqtt = mqtt;
        }

        /// <summary>
        /// GET: api/simulation/run
        /// </summary>
        /// <returns></returns>
        [HttpGet("run")]
        public async Task<JsonResult> Run()
        {
            try
            {
                SetupSimulation();

                RunSimulationAsync();
            }
            catch(Exception ex)
            {
                await _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, $"The Simulation had an exception: {ex.Message}"));
            }

            return new JsonResult(_runtime.Arguments);
        }

        private void SetupSimulation()
        {
            // fetch the array of arguments for each simulator 
            var args = SessionArguments();

            // setup the simulators and attach them to the runtime, based on the existent args
            _runtime = _factory.SetupSimulation(args);
            _runtime.Started += OnStarted;
            _runtime.Stopped += OnStopped;
            _runtime.IterationPassed += OnIterationPassed;

            _runtime.Simulators.Items.ForEach(s => s.Executed += OnSimulatorExecuted);
        }

        private void RunSimulationAsync()
        {
            if (_runtime.Validate() == false)
            {
                throw new Exception("The validation of the simulation failed");
            }

            // run until break condition
            var task = _runtime.RunAsync(ITERATIONS);
        }

        private void OnStarted(object sender, SimulatorEventArgs e)
        {
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(e.Timestamp, "# Simulation started"));
        }

        private void OnStopped(object sender, SimulatorEventArgs e)
        {
            this.HttpSession().SetArguments(_factory.Arguments);
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, "# Simulation stopped"));
        }

        private void OnSimulatorExecuted(object sender, SimulatorEventArgs e)
        {
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, $"{e.Arguments.Name} passed"));
            if (e.Arguments is AdaptedFriisArgs)
            {
                var channelArgs = e.Arguments as AdaptedFriisArgs;
                _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, $"{channelArgs.RadioBox.TotalData} data points calculated"));
            }
        }

        private void OnIterationPassed(object sender, SimulatorEventArgs e)
        {
            var runArgs = e.Arguments as RuntimeArgs;
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, $"Iteration {runArgs.Iterations} done.<br />"));
        }

        private string ToMessage(DateTime timestamp, string message)
        {
            return $"{timestamp} - {message}";
        }
    }
}
