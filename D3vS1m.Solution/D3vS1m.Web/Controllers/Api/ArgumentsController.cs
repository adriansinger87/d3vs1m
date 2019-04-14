using D3vS1m.Domain.Data.Arguments;
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
        public ArgumentsController() : base()
        {
        }

        // GET: api/Arguments/id
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            var simulator = Context.SimulatorRepo[id];
            return new JsonResult(simulator.Arguments);
        }

        // PUT: api/Arguments/id
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] string value)
        {
            // TODO: write data to a session

            try
            {
                var simulator = Context.SimulatorRepo[id];
                var type = simulator.Arguments.GetType();
                var args = JsonConvert.DeserializeObject(value, type);

                if (args == null)
                {
                    throw new Exception("value could not be deserialized");
                }
                simulator.With(args as ArgumentsBase);
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