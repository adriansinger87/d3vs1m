using D3vS1m.Application;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sin.Net.Persistence.IO;
using System;
using System.Linq;

namespace D3vS1m.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArgumentsController : ApiControllerBase
    {
        public ArgumentsController(IHostingEnvironment env, FactoryBase factory) : base(env, factory)
        {

        }

        // GET: api/Arguments
        [HttpGet]
        public JsonResult Get()
        {
            var args = SessionArguments();

            if (args == null)
            {
                args = _factory.GetPredefinedArguemnts();
                this.HttpSession().SetArguments(args);
            }

            return new JsonResult(args);
        }

        // GET: api/Arguments/id
        [HttpGet("{guid}")]
        public JsonResult Get(string guid)
        {
            var arg = SessionArguments().GetByGuid(guid);

            return new JsonResult(arg);
        }

        // PUT: api/Arguments/id
        [HttpPut("{guid}")]
        public void Put(string guid, [FromBody] string value)
        {
            var args = SessionArguments();
            var currentArg = args.GetByGuid(guid);
            var clientArg = JsonConvert.DeserializeObject(value, currentArg.GetType()) as ArgumentsBase;

            if (clientArg == null)
            {
                throw new Exception("value could not be deserialized");
            }
            else
            {
                args.SetByGuid(guid, clientArg);
            }

            // safe session
            this.HttpSession().SetArguments(args);
        }

        private ArgumentsBase[] SessionArguments()
        {
            return this.HttpSession().GetArguments();
        }

    }
}