using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulationController : ApiControllerBase
    {

        public SimulationController(IHostingEnvironment env, FactoryBase factory) : base(env, factory)
        {
            _factory = factory;
        }

        // GET: api/simulation/run
        [HttpGet("run")]
        public JsonResult Run()
        {
            return new JsonResult(_factory);
        }

       
    }
}
