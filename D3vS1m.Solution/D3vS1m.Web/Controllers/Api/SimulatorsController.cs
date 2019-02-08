using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorsController : ControllerBase
    {
        private const string CONTEXT = "CONTEXT";

        D3vS1mFacade _data;

        public SimulatorsController()
        {
            _data = new D3vS1mFacade();

            // TODO implement and bugfix session 
            //if (HttpContext.Session.Keys.Contains(CONTEXT) == false)
            //{
            //    _data = new D3vS1mFacade();
            //    HttpContext.Session.SetData(CONTEXT, _data);
            //}
            //else
            //{
            //    _data = HttpContext.Session.GetData<D3vS1mFacade>(CONTEXT);
            //}
               
            
            _data.RegisterPredefined();
        }


        // GET: api/Simulators
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_data.SimulatorRepo);
        }

        // GET: api/Simulators/id
        [HttpGet("{id}", Name = "GetById")]
        public JsonResult Get(string id)
        {
            var simulator = _data.SimulatorRepo[id];
            return new JsonResult(simulator);
            
        }

        // GET: api/Simulators/model/0
        [HttpGet("model/{model}", Name = "GetByModel")]
        public JsonResult Get(int model)
        {
            ISimulatable simulator = null;
            foreach (ISimulatable sim in _data.SimulatorRepo)
            {
                if (sim.Model == (SimulationModels)model)
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
