using Microsoft.AspNetCore.Mvc;

namespace Hackinder.Controllers
{
    [Produces("application/json")]
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "Api works";
        }
    }
}