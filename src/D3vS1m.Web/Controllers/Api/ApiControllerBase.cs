using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sin.Net.Domain.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace D3vS1m.Web.Controllers.Api
{
    public class ApiControllerBase : ControllerBase
    {
        // -- fields

        private const string CONTEXT = "Arguments";
        protected readonly IHostingEnvironment _hostingEnvironment;
        private readonly string _webRootPath;
        private readonly string _dataPath;

        // -- constructor

        /// <summary>
        /// Constructor that initializes the paths on the web server for perstence access
        /// </summary>
        /// <param name="p_env">hosting environemnt with path information</param>
        public ApiControllerBase(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            _webRootPath = Path.Combine(_hostingEnvironment.WebRootPath);
            _dataPath = Path.Combine(_hostingEnvironment.ContentRootPath, "App_Data");
        }

        // -- methods

        #region Session
        /// <summary>
        /// Gets a data object from the session by its key or returns the default value of the type T
        /// </summary>
        /// <typeparam name="T">The target type T</typeparam>
        /// <param name="key">The key as an accesor to the session</param>
        /// <returns>The data object or the default value of T</returns>
        protected T GetOrDefault<T>(string key) where T : new()
        {
            T data;
            try
            {
                if (HttpContext.Session.Keys.Contains(key) == false)
                {
                    data = default(T);
                }
                else
                {
                    data = HttpContext.Session.GetData<T>(key);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                Log.Error($"returning default after exception");
                data = default(T);
            }
            return data;
        }

        /// <summary>
        /// Gets a data object from the session by its key or returns a new instance of the type T
        /// </summary>
        /// <typeparam name="T">The target type T</typeparam>
        /// <param name="key">The key as an accesor to the session</param>
        /// <returns>The data object or a new instance of T</returns>
        protected T GetOrNew<T>(string key, out bool created) where T : new()
        {
            T data;
            created = false;
            try
            {
                if (HttpContext.Session.Keys.Contains(key) == false)
                {
                    data = new T();
                    HttpContext.Session.SetData(key, data);
                    created = true;
                }
                else
                {
                    data = HttpContext.Session.GetData<T>(key);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                Log.Error($"returning new object instance after exception");
                data = new T();
            }
            return data;
        }

        /// <summary>
        /// Sets an object of type T and stores it unter the key parameter in the session
        /// </summary>
        /// <typeparam name="T">The target type T</typeparam>
        /// <param name="key">The key as accessor</param>
        /// <param name="data">The data object</param>
        protected void Set<T>(string key, T data)
        {
            try
            {
                HttpContext.Session.SetData(key, data);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
        }
        #endregion

        public void Load(out D3vS1mFacade context)
        {
            context = GetContext();
        }

        protected void Save(D3vS1mFacade context)
        {
            SetContext(context);
        }

        // -- private methods

        private List<ArgumentsBase> GetContext()
        {          
            var context = GetOrNew<List<ArgumentsBase>>(CONTEXT, out bool created);

            if (created)
            {

            }

            return context;
        }

        private void SetContext(List<ArgumentsBase> arguments)
        {
            Set(CONTEXT, arguments);
        }

    }
}
