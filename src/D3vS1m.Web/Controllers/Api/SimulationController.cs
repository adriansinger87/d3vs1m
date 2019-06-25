using D3vS1m.Application;
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
        public SimulationController(IHostingEnvironment env, FactoryBase factory) : base(env, factory)
        {
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

            if (runtime.Validate() == false)
            {
                throw new Exception("The validation of the simulation failed");
            }

            // run only once
            var task = runtime.RunAsync(1);

            try
            {
                task.Wait();
            }
            catch (Exception aex)
            {
                throw aex;
            }

            return new JsonResult(runtime.Arguments);
        }
    }
}
