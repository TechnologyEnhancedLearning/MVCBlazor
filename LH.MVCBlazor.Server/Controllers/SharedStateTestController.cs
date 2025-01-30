using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Package.LH.Services.StateServices;

namespace LH.MVCBlazor.Server.Controllers
{
    public class SharedStateTestController : Controller
    {
        private readonly ILHS_AttendeesStateService _attendeesStateService;
        private static int count = 0;

        //This will be the prerender service only
        public SharedStateTestController(ILHS_AttendeesStateService attendeesStateService)
        {
            _attendeesStateService = attendeesStateService;
        }

        public async Task<IActionResult> Index()
        {

            // Load the list of attendees from the service
            var viewModel = new AttendeesViewModel { Attendees = (await _attendeesStateService.GetAttendeesAsync()).Data };
            ViewBag.Count = count;
            return View(viewModel);
        }

        public ActionResult IncrementControllerButtonHitCount()
        {

            count++;
            return RedirectToAction("Index"); // Reload the view
        }
    }
}
