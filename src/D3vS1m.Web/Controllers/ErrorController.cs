using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using D3vS1m.Web.Models;

namespace MPM.Web.Injection.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        public ErrorController()
        {

        }

        public IActionResult Index()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var error = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = Response.StatusCode,
                StatusMessage = ReasonPhrases.GetReasonPhrase(Response.StatusCode),
                Exception = feature?.Error
            };

            return View(error);
        }
    }
}