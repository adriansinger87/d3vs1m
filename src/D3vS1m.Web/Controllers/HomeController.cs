using Microsoft.AspNetCore.Mvc;
using System;

namespace D3vS1m.WebAPI.Controllers
{
    public class HomeController : ViewControllerBase
    {
        public IActionResult Index()
        {
            base.SetViewBag();
            return View();
        }

        public IActionResult Foo()
        {
            throw new Exception("exception in Foo()");
        }
    }
}
