using D3vS1m.Application;
using D3vS1m.Domain.Data.Arguments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sin.Net.Domain.Logging;
using System;
using System.Linq;
using System.Text;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArgumentsController : ApiControllerBase
    {
        public ArgumentsController(IHostingEnvironment env, D3vS1mFactory factory) : base(env, factory)
        {

        }

        // GET: api/Arguments
        [HttpGet]
        public JsonResult Get()
        {
            LoadContext(out ArgumentsBase[] args);
            return new JsonResult(args);
        }

        // GET: api/Arguments/id
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            LoadContext(out ArgumentsBase[] args);
            return new JsonResult(args);
        }

        // PUT: api/Arguments/id
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            try
            {
                LoadContext(out ArgumentsBase[] arguments);

                var simulator = _factory.Simulators[id];
                var type = simulator.Arguments.GetType();
                var args = JsonConvert.DeserializeObject(value, type);

                if (args == null)
                {
                    throw new Exception("value could not be deserialized");
                }

                simulator.With(args as ArgumentsBase);

                // safe session
                SaveContext(arguments);
            }
            catch (Exception ex)
            {
                Log.Error("caught exception", this);
                Log.Fatal(ex);

                HttpContext.Response.StatusCode = 500;
                HttpContext.Response.Body.Write(
                    Encoding.ASCII.GetBytes(ex.Message));
            }
            
        }


    }
}