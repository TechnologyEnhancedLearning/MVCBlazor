using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Package.LH.Entities.Models;
using Package.LH.Services.StateServices;
using Package.Shared.BlazorComponents.Enums;

namespace LH.MVCBlazor.Server.Controllers
{
    public class AttendeesController : Controller
    {
        private readonly ILHS_AttendeesStateService _attendeesStateService;

        public AttendeesController(ILHS_AttendeesStateService attendeesStateService)
        {
            _attendeesStateService = attendeesStateService;
        }

        //Here the MVCRendered is added because we can render these same modes in our Blazor pages and we dont have to have seperate path stems for our blazor pages
        //So to illustrate this it is in the page name
        
        //Get
        [HttpGet("/Attendees/Static-MVCRendered")]
        [HttpGet("/Attendees/Server-MVCRendered")]
        [HttpGet("/Attendees/ServerPrendered-MVCRendered")]
        [HttpGet("/Attendees/WebAssembly-MVCRendered")]
        [HttpGet("/Attendees/WebAssemblyPrerendered-MVCRendered")]
        //Post
        [HttpPost("/Attendees/Static-MVCRendered")]
        [HttpPost("/Attendees/Server-MVCRendered")]
        [HttpPost("/Attendees/ServerPrendered-MVCRendered")]
        [HttpPost("/Attendees/WebAssembly-MVCRendered")]
        [HttpPost("/Attendees/WebAssemblyPrerendered-MVCRendered")]
        public async Task<IActionResult> Index()
        {
            // Get the current route from the request path
            string route = HttpContext.Request.Path.Value; // E.g., "/Attendees/Static-MVCRendered"

            // Extract the part of the route that matches the render mode
            string renderModeString = route.Replace("/Attendees/", "").Replace("-MVCRendered", "");

            // Try to parse the extracted string into the enum
            if (Enum.TryParse(renderModeString, true, out GB_ComponentTagRenderMode renderMode))
            {
                // Successfully parsed render mode
            }
            else
            {
                // Default to Static if no match
                renderMode = GB_ComponentTagRenderMode.Static;
                //This could be the noJs so lets do static as a fallback
                //throw new Exception("Rendermode not in the enum"); //this is just for convenience we wouldnt have render mode routes
            }

            // Load the list of attendees from the service
            var viewModel = new AttendeesViewModel { Attendees = (await _attendeesStateService.GetAttendeesAsync()).Data};
            ViewBag.RenderMode = renderMode.ToString();
            return View(viewModel);
        }



    }
}
