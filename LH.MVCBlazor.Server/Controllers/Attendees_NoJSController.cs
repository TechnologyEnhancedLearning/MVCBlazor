using LH.MVCBlazor.Server.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;

namespace LH.MVCBlazor.Server.Controllers
{
    [Route("Attendees/NoJS")] //Throughout would be nice to clearly show the difference as a seperate route
    public class Attendees_NoJSController : NoJSBaseController
    {

        private readonly IAttendeeStateService _attendeeStateService;


        protected override string DefaultRouteController { get; set; } = "Attendees";
        protected override string DefaultRouteAction { get; set; } = "Index";

      
      
        public Attendees_NoJSController(IAttendeeStateService attendeeStateService, BlazorPageRegistryService blazorPageRegistryService)
            : base(blazorPageRegistryService)
        {
            _attendeeStateService = attendeeStateService;
        }
        //qqqq initial focus just on blazor
        // POST: /Attendees_NoJS/RemoveAttendee 
        [HttpPost]
        public async Task<IActionResult> RemoveAttendeeByTemporaryId(Guid clientTemporaryId, string returnUrl = null)
        {
            await _attendeeStateService.RemoveAttendeeByTemporaryIdAsync(clientTemporaryId);
            await _attendeeStateService.ReplaceDbWithListAsync();//we are not persisting in the controller

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> AddAttendee(LH_AttendeeModel newAttendee, string returnUrl = null)
        {

            await _attendeeStateService.AddAttendeeAsync(newAttendee);
            await _attendeeStateService.ReplaceDbWithListAsync();//we are not persisting in the controller

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);

        }

   
        [HttpPost]
        public async Task<IActionResult> SaveAttendees(string returnUrl = null)
        {
            await _attendeeStateService.ReplaceDbWithListAsync(); // Ensure all changes are saved

            //for now for ease lets do this but it wouldnt work for bookmarks etc
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);
        }
    }
}
