using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Package.LH.Entities.Models;
using Package.LH.Services.StateServices;
using Package.Shared.BlazorComponents.Enums;
using static LH.MVCBlazor.Server.Helpers.ControllerHelpers.ControllerHelper;

namespace LH.MVCBlazor.Server.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly ILHS_AttendeesStateService _attendeesStateService;

        public AttendeesController(ILHS_AttendeesStateService attendeesStateService)
        {
            _attendeesStateService = attendeesStateService;
        }
        
        //Get
        [HttpGet("/Attendees/Static-MVCRendered")]
        [HttpGet("/Attendees/Server-MVCRendered")]
        [HttpGet("/Attendees/ServerPrerendered-MVCRendered")]
        [HttpGet("/Attendees/WebAssembly-MVCRendered")]
        [HttpGet("/Attendees/WebAssemblyPrerendered-MVCRendered")]
        //Post
        [HttpPost("/Attendees/Static-MVCRendered")]
        [HttpPost("/Attendees/Server-MVCRendered")]
        [HttpPost("/Attendees/ServerPrerendered-MVCRendered")]
        [HttpPost("/Attendees/WebAssembly-MVCRendered")]
        [HttpPost("/Attendees/WebAssemblyPrerendered-MVCRendered")]
        [Route("Attendees")]
        public async Task<IActionResult> Index()
        {

            AttendeesViewModel attendeesData = null;
            if (TempData["AttendeesData"] != null)
            {
                attendeesData = JsonConvert.DeserializeObject<AttendeesViewModel>(TempData["AttendeesData"].ToString());
            }

            // Load the list of attendees from the service
            var viewModel = attendeesData ?? new AttendeesViewModel { Attendees = (await _attendeesStateService.GetAttendeesAsync()).Data};
            ViewBag.RenderMode = GetRenderModeStr(HttpContext.Request.Path.Value,"Attendees");
            return View(viewModel);
        }

        [HttpPost("/Attendees/MVCRenderedBlazorPage")]
        [HttpGet("/Attendees/MVCRenderedBlazorPage")]
        public async Task<IActionResult> MVCRenderedBlazorPageView()
        {
            AttendeesViewModel attendeesData = null;
            if (TempData["AttendeesData"] != null)
            {
                attendeesData = JsonConvert.DeserializeObject<AttendeesViewModel>(TempData["AttendeesData"].ToString());
            }

            // Load the list of attendees from the service
            var viewModel = attendeesData ?? new AttendeesViewModel { Attendees = (await _attendeesStateService.GetAttendeesAsync()).Data };
            ViewBag.RenderMode = GB_ComponentTagRenderMode.WebAssemblyPrerendered.ToString();

            return View("MVCRenderedBlazorPageView", viewModel);
        }

    }
}
