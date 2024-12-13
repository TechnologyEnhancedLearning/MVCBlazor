using LH.MVCBlazor.Server.Controllers.BaseControllers;
using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Package.LH.BlazorComponents.DependencyInjection;
using Package.LH.Entities.Models;
using Package.LH.Entities.Models.FormModels;
using Package.LH.Services.StateServices;
using Package.Shared.BlazorComponents.Enums;

namespace LH.MVCBlazor.Server.Controllers
{
    [Route("Attendees/NoJS")]
    public class Attendees_NoJSController : NoJSBaseController
    {

        private readonly ILHS_AttendeesStateService _LHS_AttendeesStateService;

        protected override string DefaultViewRouteController { get; set; } = "~/Views/Attendees/Index.cshtml";
        protected override string DefaultRouteController { get; set; } = "Attendees";
        protected override string DefaultRouteAction { get; set; } = "Index";
       
        protected override string RedirectBlazorPagesNoJsStaticMVCRoute { get; set ; } = "/Attendees/Static-MVCRendered";

        public Attendees_NoJSController(ILHS_AttendeesStateService LHS_AttendeesStateService, LHB_BlazorPageRegistryService blazorPageRegistryService)
            : base(blazorPageRegistryService)
        {
            this._LHS_AttendeesStateService = LHS_AttendeesStateService;
        }
  
        [HttpPost("RemoveAttendeeByTemporaryId")]
        public async Task<IActionResult> RemoveAttendeeByTemporaryId(Guid clientTemporaryId, string returnUrl = null)
        {
            await _LHS_AttendeesStateService.RemoveAttendeeByTemporaryIdAsync(clientTemporaryId);
            await _LHS_AttendeesStateService.ReplaceDBWithListAsync();//we are not persisting in the controller

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);
        }

        [HttpPost("AddAttendee")]
        public async Task<IActionResult> AddAttendee(LH_AttendeeFormModel newAttendee, string returnUrl = null)
        {

            if (!ModelState.IsValid)
            {

                newAttendee.ModelStateErrors = GetModelStateDictionary(ModelState);

                var viewModel = new AttendeesViewModel((await _LHS_AttendeesStateService.GetAttendeesAsync()).Data, newAttendee);

                ViewBag.RenderMode = GetRenderModeStr();

                bool IsBlazorPage = true;
                if (IsBlazorPage)
                {
                    //cant pass the validation data with current set up so redirect to the static page
                    TempData["AttendeesData"] = JsonConvert.SerializeObject(viewModel); // Serialize to pass complex objects
                    return RedirectToAction("Static-MVCRendered", "Attendees");


                }
                else
                {
                    //its mvc so just return model to where it came from now it has validation state errors on it

                    return ReturnViewWithModel(viewModel);
                }

            }





            await _LHS_AttendeesStateService.AddAttendeeAsync(newAttendee);
            await _LHS_AttendeesStateService.ReplaceDBWithListAsync();//we are not persisting in the controller

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);

        }


        [HttpPost("SaveAttendees")]
        public async Task<IActionResult> SaveAttendees(string returnUrl = null) //everything is posted at everystage to update anyway
        {
            await _LHS_AttendeesStateService.ReplaceDBWithListAsync(); // Ensure all changes are saved

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);
        }

        private string GetRenderModeStr()
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

            }

            return renderMode.ToString();
        }
    }
}
