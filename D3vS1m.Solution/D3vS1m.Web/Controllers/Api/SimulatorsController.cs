
using D3vS1m.Application;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Microsoft.AspNetCore.Mvc;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorsController : ApiControllerBase
    {
        public SimulatorsController() : base()
        {
        }

        // GET: api/Simulators
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(Context.SimulatorRepo);
        }

        // GET: api/Simulators/id
        [HttpGet("{id}", Name = "GetById")]
        public JsonResult Get(string id)
        {
            var simulator = Context.SimulatorRepo[id];
            return new JsonResult(simulator);
        }

        // GET: api/Simulators/id/args
        [HttpGet("{id}", Name = "GetArgs")]
        [Route("args")]
        public JsonResult GetArgs([FromQuery(Name = "id")] string id)
        {
            var simulator = Context.SimulatorRepo[id];
            // TODO: sort name as first property and then all other properties or create a view model 
            return new JsonResult(simulator.Arguments);
        }

        // GET: api/Simulators/model/0
        [HttpGet("model/{model}", Name = "GetByModel")]
        public JsonResult Get(int model)
        {
            ISimulatable simulator = null;
            foreach (ISimulatable sim in Context.SimulatorRepo)
            {
                if (sim.Type == (SimulationModels)model)
                {
                    simulator = sim;
                    break;
                }
            }
            return new JsonResult(simulator);

        }

        // POST: api/Simulators
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Simulators/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
