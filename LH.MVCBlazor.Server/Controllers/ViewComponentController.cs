using Microsoft.AspNetCore.Mvc;

namespace LH.MVCBlazor.Server.Controllers
{
    public class ViewComponentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
