using D3vS1m.Application;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Extensions;
using D3vS1m.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Persistence.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D3vS1m.Web.Extensions;
using Newtonsoft.Json.Linq;
using Sin.Net.Persistence.IO;
using Sin.Net.Persistence.IO.Json;

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
        public ActionResult Get()
        {
            var args = _factory.GetPredefinedArguments();
            SetNetworkFromJson(args);

            JsonIO.EnableCaseResolver = true; 
            var argsJson = JsonIO.ToJsonString(args, HttpSessionExtensions.ArgumentsBinder);

            return Content(argsJson, "application/json");  ;
        }

        /// TODO: will be remove after code review
        /// <summary>
        /// GET: api/Arguments/{id}
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        /*
        [HttpGet]
        public JsonResult Get(string guid)
        {
            var arg = SessionArguments().GetByGuid(guid);

            if (arg is AdaptedFriisArgs)
            {
                var ca = arg as AdaptedFriisArgs;
                var channelArg = new AdaptedFriisArgsView(ca);
                return new JsonResult(channelArg);
            }
            else
            {
                return new JsonResult(arg);
            }
        }
        */


        /// TODO: will be remove after code review
        /// <summary>
        /// PUT: api/Arguments/id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        //[HttpPut("{guid}")]
        //public void Put(string guid, [FromBody] string value)
        //{
        //    var args = SessionArguments();
        //    var currentArg = args.GetByGuid(guid);
        //    var clientArg = JsonConvert.DeserializeObject(value, currentArg.GetType()) as ArgumentsBase;

        //    if (clientArg == null)
        //    {
        //        throw new Exception("value could not be deserialized");
        //    }
        //    else if (clientArg is AdaptedFriisArgs)
        //    {
        //        (clientArg as AdaptedFriisArgs).UpdatePositions();
        //    }

        //    args.SetByGuid(guid, clientArg);


        //    // safe session
        //    this.HttpSession().SetArguments(args);
        //}
        
        //TODO: should ask Adrian to change location
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
