using System;
using ELI.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ELI.API.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            try
            {
                var uri = new Uri("/swagger", UriKind.Relative);
                return Redirect(uri.ToString());
            }
            catch (Exception ex)
            {
                new ExceptionHandlingService(ex, null, null).LogException();
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
