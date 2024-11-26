using Microsoft.AspNetCore.Mvc;

namespace LH.DB.API.Controllers
{
    public class LH_CharactersDBController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
