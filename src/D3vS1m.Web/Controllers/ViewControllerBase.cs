using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace D3vS1m.WebAPI.Controllers
{
    public abstract class ViewControllerBase : Controller
    {
        protected void SetViewBag()
        {
            ViewBag.Title = "D3vS1m";
            ViewBag.Message = "";
        }
    }
}
