using D3vS1m.Application;
using D3vS1m.Application.Devices;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Persistence.IO.Json;
using Sin.Net.Persistence.Settings;
using System.Collections.Generic;
using D3vS1m.WebAPI.Extensions;

namespace D3vS1m.WebAPI.Controllers.Api
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
            var args = _factory.SimulationArguments;
            SetNetworkFromJson(args);

            JsonIO.EnableCaseResolver = true;
            var argsJson = JsonIO.ToJsonString(args, HttpSessionExtensions.ArgumentsBinder);

            return Content(argsJson, "application/json");
        }

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
