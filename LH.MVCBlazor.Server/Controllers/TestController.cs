using Microsoft.AspNetCore.Mvc;

namespace LH.MVCBlazor.Server.Controllers
{
    public class TestController : Controller
    {
        [Route("Test")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
