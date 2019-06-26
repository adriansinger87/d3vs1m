using D3vS1m.Application;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Extensions;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sin.Net.Domain.Logging;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;

namespace D3vS1m.Web.Controllers.Api
{
    /// <summary>
    /// This class represents the rest-api for loading and manipulating a list of arguments.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ArgumentsController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="factory"></param>
        public ArgumentsController(IHostingEnvironment env, FactoryBase factory) : base(env, factory)
        {

        }
        
        /// <summary>
        /// GET: api/Arguments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get()
        {
            var args = SessionArguments();

            if (args == null)
            {
                args = _factory.GetPredefinedArguments();

                // --- individual arguments setups

                // -- network
                SetNetworkFromJson(args);

                this.HttpSession().SetArguments(args);
            }

            return new JsonResult(args);
        }

        /// <summary>
        /// GET: api/Arguments/{id}
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        public JsonResult Get(string guid)
        {
            var arg = SessionArguments().GetByGuid(guid);

            return new JsonResult(arg);
        }
        
        /// <summary>
        /// PUT: api/Arguments/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
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

        private void SetNetworkFromJson(ArgumentsBase[] args)
        {
            var netKey = D3vS1m.Application.Models.Network.Key;
            if (!args.ContainsName(netKey))
            {
                Log.Error($"Could not find the network arguments with the key '{netKey}'");
                return;
            }

            // fetch the arguments from the array
            var netArgs = args.GetByName(netKey) as NetworkArgs;

            // load a network file
            var setting = new JsonSetting
            {
                Location = "App_Data",
                Name = "devices.json"
            };
            var devices = base.Persistence
                .Importer(Sin.Net.Persistence.Constants.Json.Key)
                .Setup(setting)
                .Import()
                .As<List<BasicDevice>>()
                .ToArray();

            // set the network
            netArgs.Network.Clear();
            netArgs.Network.AddRange(devices);
        }
    }
}