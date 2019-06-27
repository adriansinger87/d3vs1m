using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Infrastructure.Mqtt;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ApiControllerBase
    {
        private const string CONSOLE_TOPIC = "d3vs1m/console"; 

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
            // fetch the array of arguments for each simulator 
            var args = SessionArguments();

            // setup the simulators and attach them to the runtime, based on the existent args
            var runtime = _factory.SetupSimulation(args);
            runtime.Started += OnStarted;
            runtime.Stopped += OnStopped;
            runtime.IterationPassed += OnIterationPassed;

            runtime.Simulators.Items.ForEach(s => s.Executed += OnSimulatorExecuted);

            if (runtime.Validate() == false)
            {
                throw new Exception("The validation of the simulation failed");
            }

            // run only once
            var task = runtime.RunAsync(10);

            return new JsonResult(runtime.Arguments);
        }

        private void OnStarted(object sender, SimulatorEventArgs e)
        {
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(e.Timestamp, "Simulation started"));
        }

        private void OnStopped(object sender, SimulatorEventArgs e)
        {
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, "Simulation stopped"));
        }

        private void OnSimulatorExecuted(object sender, SimulatorEventArgs e)
        {
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, $"Simulation '{e.Arguments.Name}' passed"));
        }

        private void OnIterationPassed(object sender, SimulatorEventArgs e)
        {
            var runArgs = e.Arguments as RuntimeArgs;
            _mqtt.PublishAsync(CONSOLE_TOPIC, ToMessage(DateTime.Now, $"Iteration {runArgs.Iterations} done."));
        }

        private string ToMessage(DateTime timestamp, string message)
        {
            return $"{timestamp} - {message}";
        }
    }
}
