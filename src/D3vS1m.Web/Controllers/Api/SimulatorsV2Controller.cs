using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/v2/simulators")]
    [ApiController]
    public class SimulatorsV2Controller : ApiControllerBase
    {

        public SimulatorsV2Controller(IHostingEnvironment env) : base(env)
        {

        }


        // GET: api/v2/simulators
        [HttpGet]
        public JsonResult Get()
        {
            LoadContext(out D3vS1mFacade context);

            // redefined view model
            var list = context.Simulators.Items.Select(s => new {
                s.Name,
                s.Id,
                Type = s.Type.ToString(),
                s.Guid
            });

            return new JsonResult(list);

        }

        // GET: api/v2/Simulators/index
        [HttpGet("{index}", Name = "v2_GetByIndex")]
        public JsonResult Get(int index)
        {
            LoadContext(out D3vS1mFacade context);
            var simulator = context.Simulators[index];
            return new JsonResult(simulator);
        }

    }
}
