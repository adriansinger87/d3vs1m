using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace D3vS1m.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static ISession HttpSession(this ControllerBase controller)
        {
            if (controller.HttpContext == null)
            {
                throw new FieldAccessException("HttpContext was not present to load the session.");
            }
            return controller.HttpContext.Session;
        }
    }
}
