using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using D3vS1m.Web.Models;
using Sin.Net.Domain.Logging;

namespace D3vS1m.Web.Controllers
{
    [Route("Error")]
    public class ErrorController : ViewControllerBase
    {
        public ErrorController()
        {

        }

        public IActionResult Index()
        {
            base.SetViewBag();
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var error = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = Response.StatusCode,
                StatusMessage = ReasonPhrases.GetReasonPhrase(Response.StatusCode),
                Exception = feature?.Error
            };

            Log.Error($"An error occured with status {error.StatusCode} - {error.StatusMessage}", this);
            if (error.Exception != null)
            {
                Log.Fatal(error.Exception);
            }

            return View(error);
        }
    }
}