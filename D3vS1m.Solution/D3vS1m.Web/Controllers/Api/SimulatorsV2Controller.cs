using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/v2/simulators")]
    [ApiController]
    public class SimulatorsV2Controller : ControllerBase
    {
        D3vS1mFacade _data;

        public SimulatorsV2Controller()
        {
            _data = new D3vS1mFacade();
            _data.RegisterPredefined(new RuntimeController(new D3vS1mValidator()));
        }


        // GET: api/v2/simulators
        [HttpGet]
        public JsonResult Get()
        {
            var list = _data.SimulatorRepo.Items.Select(s => new {
                s.Name,
                s.Id,
                Type = s.Type.ToString(),
                s.Guid
            });
            return new JsonResult(list);

        }

        // GET: api/v2/Simulators/index
        [HttpGet("{index}", Name = "v2_GetById")]
        public JsonResult Get(int index)
        {
            // TODO: try catch absicherung
            var simulator = _data.SimulatorRepo[index];
            return new JsonResult(simulator);

        }

    }
}
