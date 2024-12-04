using LH.MVCBlazor.Server.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using Package.LH.BlazorComponents.DependencyInjection;
using Package.LH.Entities.Models;
using Package.LH.Services.StateServices;

namespace LH.MVCBlazor.Server.Controllers
{
    [Route("Attendees/NoJS")] //Throughout would be nice to clearly show the difference as a seperate route
    public class Attendees_NoJSController : NoJSBaseController
    {

        private readonly ILHS_AttendeesStateService LHS_AttendeesStateService;


        protected override string DefaultRouteController { get; set; } = "Attendees";
        protected override string DefaultRouteAction { get; set; } = "Index";
        protected override string DefaultViewRouteController { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        protected override string RedirectBlazorPagesNoJsStaticMVCRoute { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Attendees_NoJSController(ILHS_AttendeesStateService LHS_AttendeesStateService, LHB_BlazorPageRegistryService blazorPageRegistryService)
            : base(blazorPageRegistryService)
        {
            this.LHS_AttendeesStateService = LHS_AttendeesStateService;
        }
        //qqqq initial focus just on blazor
        // POST: /Attendees_NoJS/RemoveAttendee 
        [HttpPost]
        public async Task<IActionResult> RemoveAttendeeByTemporaryId(Guid clientTemporaryId, string returnUrl = null)
        {
            await LHS_AttendeesStateService.RemoveAttendeeByTemporaryIdAsync(clientTemporaryId);
            await LHS_AttendeesStateService.ReplaceDBWithListAsync();//we are not persisting in the controller

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendee(LH_AttendeeModel newAttendee, string returnUrl = null)
        {

            await LHS_AttendeesStateService.AddAttendeeAsync(newAttendee);
            await LHS_AttendeesStateService.ReplaceDBWithListAsync();//we are not persisting in the controller

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);

        }

   
        [HttpPost]
        public async Task<IActionResult> SaveAttendees(string returnUrl = null) //everything is posted at everystage to update anyway
        {
            await LHS_AttendeesStateService.ReplaceDBWithListAsync(); // Ensure all changes are saved

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);
        }
    }
}
