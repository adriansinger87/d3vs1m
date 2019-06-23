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
    public class SimulationController : ControllerBase
    {
        IHostingEnvironment _env;
        FactoryBase _factory;

        public SimulationController(IHostingEnvironment env, FactoryBase factory)
        {
            _env = env;
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
