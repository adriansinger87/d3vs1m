using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace D3vS1m.Web.Controllers
{
    public abstract class ViewControllerBase : Controller
    {
        public void SetViewBag()
        {
            ViewBag.Title = "D3vS1m";
            ViewBag.Version = this.GetType().Assembly.GetName().Version;
        }
    }
}
