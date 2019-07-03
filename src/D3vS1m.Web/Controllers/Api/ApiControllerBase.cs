using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sin.Net.Domain.IO;
using Sin.Net.Domain.Logging;
using Sin.Net.Persistence;
using System;
using System.IO;
using System.Linq;

namespace D3vS1m.Web.Controllers.Api
{
    public class ApiControllerBase : ControllerBase
    {
        // -- fields

        protected readonly IHostingEnvironment _hostingEnvironment;
        protected FactoryBase _factory;

        private IPersistenceControlable _io;

        protected readonly string _webRootPath;
        protected readonly string _dataPath;

        // -- constructor

        /// <summary>
        /// Constructor that initializes the paths on the web server for perstence access
        /// </summary>
        /// <param name="p_env">hosting environemnt with path information</param>
        public ApiControllerBase(IHostingEnvironment env, FactoryBase factory)
        {
            _hostingEnvironment = env;
            _factory = factory;

            _webRootPath = Path.Combine(_hostingEnvironment.WebRootPath);
            _dataPath = Path.Combine(_hostingEnvironment.ContentRootPath, "App_Data");
        }

        protected ArgumentsBase[] SessionArguments()
        {
            // TODO @ AS: When the session returns nothing create new arguments
            return this.HttpSession().GetArguments();
        }


       

        public IPersistenceControlable Persistence
        {
            get
            {
                if (_io == null)
                {
                    _io = new PersistenceController();
                }
                return _io;
            }
        }
    }
}
