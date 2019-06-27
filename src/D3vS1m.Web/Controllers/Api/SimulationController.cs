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

            if (runtime.Validate() == false)
            {
                throw new Exception("The validation of the simulation failed");
            }

            // run only once
            var task = runtime.RunAsync(100);

            return new JsonResult(runtime.Arguments);
        }

        private void OnStarted(object sender, SimulatorEventArgs e)
        {
            _mqtt.PublishAsync("d3vs1m/console", $"Simulation started {e.Timestamp}");
        }

        private void OnStopped(object sender, SimulatorEventArgs e)
        {
            _mqtt.PublishAsync("d3vs1m/console", $"Simulation stopped {DateTime.Now}");
        }

        private void OnIterationPassed(object sender, SimulatorEventArgs e)
        {
            var runArgs = e.Arguments as RuntimeArgs;
            _mqtt.PublishAsync("d3vs1m/console", $"Simulation iteration {runArgs.Iterations} passed {DateTime.Now}");
        }
    }
}
