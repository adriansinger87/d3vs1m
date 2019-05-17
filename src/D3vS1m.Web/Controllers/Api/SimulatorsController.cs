
using D3vS1m.Application;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorsController : ApiControllerBase
    {
        public SimulatorsController(IHostingEnvironment env) : base(env)
        {
        }

        // GET: api/Simulators
        [HttpGet]
        public JsonResult Get()
        {
            LoadContext(out D3vS1mFacade context);
            return new JsonResult(context.SimulatorRepo);
        }

        // GET: api/Simulators/id
        [HttpGet("{id}", Name = "GetById")]
        public JsonResult Get(string id)
        {
            LoadContext(out D3vS1mFacade context);

            var simulator = context.SimulatorRepo[id];
            return new JsonResult(simulator);
        }

        // GET: api/Simulators/args?id=...
        [HttpGet("{id}", Name = "GetArgs")]
        [Route("args")]
        public JsonResult GetArgs([FromQuery(Name = "id")] string id)
        {
            LoadContext(out D3vS1mFacade context);
            
            var simulator = context.SimulatorRepo[id];
            // TODO: sort name as first property and then all other properties or create a view model 
            return new JsonResult(simulator.Arguments);
        }
    }
}
