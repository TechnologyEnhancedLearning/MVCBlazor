using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Package.LH.Services.StateServices;
using Package.Shared.Services.StateServices.T_Services;

namespace LH.MVCBlazor.Server.Controllers
{
    public class SharedStateTestController : Controller
    {
        private readonly ILHS_AttendeesStateService _attendeesStateService;
        private readonly IT_StateCounterTestService t_stateCounterTestService;
        private static string CounterStateInController = "0";

        //This will be the prerender service only
        public SharedStateTestController(ILHS_AttendeesStateService attendeesStateService, IT_StateCounterTestService t_stateCounterTestService)
        {
            _attendeesStateService = attendeesStateService;
        }

        public async Task<IActionResult> Index()
        {

            // Load the list of attendees from the service
            var viewModel = new AttendeesViewModel { Attendees = (await _attendeesStateService.GetAttendeesAsync()).Data };
            ViewBag.CounterStateInController = CounterStateInController;
            //TODO qqqqqqq 
            return View(viewModel);
        }

        public ActionResult IncrementControllerState()
        {

            CounterStateInController = (int.Parse(CounterStateInController) + 1).ToString();
            return RedirectToAction("Index"); // Reload the view
        }
        public ActionResult IncrementControllerState()
        {

            CounterStateInController = (int.Parse(CounterStateInController) + 1).ToString();
            return RedirectToAction("Index"); // Reload the view
        }

    }
}
