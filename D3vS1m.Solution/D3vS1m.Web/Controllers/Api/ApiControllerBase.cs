using D3vS1m.Application;
using D3vS1m.Application.Runtime;
using D3vS1m.Application.Validation;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Sin.Net.Domain.Logging;
using System;
using System.Linq;

namespace D3vS1m.Web.Controllers.Api
{
    public class ApiControllerBase : ControllerBase
    {
        private const string CONTEXT = "CONTEXT";

        private D3vS1mFacade GetContext()
        {
            D3vS1mFacade data;
            try
            {
                if (HttpContext.Session.Keys.Contains(CONTEXT) == false)
                {
                    data = CreateContext();
                    HttpContext.Session.SetData(CONTEXT, data);
                }
                else
                {
                    data = HttpContext.Session.GetData<D3vS1mFacade>(CONTEXT);
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                Log.Error($"create new data context after exception");
                data = CreateContext();
            }

            return data;
        }

        private D3vS1mFacade CreateContext()
        {
            var data = new D3vS1mFacade();
            data.RegisterPredefined(new RuntimeController(new D3vS1mValidator()));
            return data;
        }

        public D3vS1mFacade Context
        {
            get
            { return GetContext(); }

        }
    }
}
