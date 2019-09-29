using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace D3vS1m.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {

            // get all devices
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            // get single device

            return new JsonResult(
                new { Device = $"my id is {id}" }
            );
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
            // create new device instances
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            // update existing device
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // delete existing device
        }
    }
}
