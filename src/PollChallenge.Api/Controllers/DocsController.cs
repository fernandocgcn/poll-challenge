using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PollChallenge.Api.Controllers
{
    /// <summary>
    /// Display the Poll API Documentation
    /// </summary>
    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DocsController : Controller
    {
        [Route(""), HttpGet]
        [AllowAnonymous]
        public IActionResult Swagger()
        {
            return Redirect("~/swagger");
        }
    }
}
