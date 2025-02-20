using Microsoft.AspNetCore.Mvc;

namespace LH.MVCBlazor.Server.Controllers
{
    public class InProductionLogLevelChangerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
