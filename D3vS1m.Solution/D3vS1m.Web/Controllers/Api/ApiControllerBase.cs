using D3vS1m.Application;
using D3vS1m.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                // TODO implement and bugfix session 
                if (HttpContext.Session.Keys.Contains(CONTEXT) == false &&
                    HttpContext.Session.GetData<D3vS1mFacade>(CONTEXT) == null)
                {
                    data = new D3vS1mFacade();
                    data.RegisterPredefined();

                    // save
                    HttpContext.Session.SetData(CONTEXT, data);
                }
                else
                {
                    data = HttpContext.Session.GetData<D3vS1mFacade>(CONTEXT);
                }
            }
            catch (Exception ex)
            {
                // TODO handle exception
                data = new D3vS1mFacade();
                data.RegisterPredefined();
            }
           
            return data;
        }

        public D3vS1mFacade Context
        {
            get 
            { return GetContext(); }
           
        }
    }
}
