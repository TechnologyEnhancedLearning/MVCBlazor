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
        private readonly IT_StateCounterTestService _t_stateCounterTestService;
        private string CounterStateInController = "0";

        //This will be the prerender service only
        public SharedStateTestController(ILHS_AttendeesStateService attendeesStateService, IT_StateCounterTestService t_stateCounterTestService)
        {
            _t_stateCounterTestService = t_stateCounterTestService;
            _attendeesStateService = attendeesStateService;
        }

        public async Task<IActionResult> Index()
        {

            // Load the list of attendees from the service
            var viewModel = new AttendeesViewModel { Attendees = (await _attendeesStateService.GetAttendeesAsync()).Data };
            ViewBag.CounterStateInController = CounterStateInController;
            ViewBag.CounterStateInServerService = _t_stateCounterTestService.GetCountFromServerService();
            return View(viewModel);
        }

        public ActionResult IncrementControllerState()
        {

            CounterStateInController = (int.Parse(CounterStateInController) + 1).ToString();
            return RedirectToAction("Index"); // Reload the view
        }

        public ActionResult IncrementServiceState()
        {

            _t_stateCounterTestService.IncrementCountInServerService();
            return RedirectToAction("Index"); // Reload the view
        }


    }
}
