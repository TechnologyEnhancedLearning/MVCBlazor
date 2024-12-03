using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Package.LH.BlazorComponents.DependencyInjection;

namespace LH.MVCBlazor.Server.Controllers.BaseControllers
{

    public abstract class NoJSBaseController : Controller
    {
        // Properties for default route and denied route
        protected abstract string DefaultRouteController { get; set; }
        protected abstract string DefaultRouteAction { get; set; }
        protected virtual string DeniedRouteController { get; set; } = "Home";
        protected virtual string DeniedRouteAction { get; set; } = "Error";

        //So we can have arguments probably should make methods abstract but ok for this

        protected LHB_BlazorPageRegistryService BlazorPageRegistryService { get; }

        protected NoJSBaseController(LHB_BlazorPageRegistryService blazorPageRegistryService)
        {
            BlazorPageRegistryService = blazorPageRegistryService;
        }
        protected ActionResult DefaultRedirectAction()
        {
            return RedirectToAction(DefaultRouteAction, DefaultRouteController);
        }
        protected ActionResult ReturnRedirect(string returnUrl)
        {
            //qqqq check this is safe
            // Check if the return URL is a local URL
            //!!! Warning not for production
            // Assuming returnUrl is defined and you want to trim the protocol (http:// or https://)
            string trimmedReturnUrlNotForProduction = returnUrl.Replace("https://", "")
                                                               .Replace("http://", "")
                                                                .Replace("localhost:44343", "");

            if (IsValidMVCPageRoute(trimmedReturnUrlNotForProduction) //MVC Routes
                || BlazorPageRegistryService.BlazorPageRoutes.Contains(trimmedReturnUrlNotForProduction) //Blazor page routes
                )
            {
                return Redirect(returnUrl); // Safe redirect
                                            //It would be nice if this was a controller and action
                                            // RedirectToAction
            }


            // If return URL is not local, redirect to the denied route
            return RedirectToAction(DeniedRouteController, DeniedRouteAction);
        }

        protected IActionResult RedirectToReturnUrl(string returnUrl = null)
        {
            // If returnUrl is null or empty, use the Referer header from the request (or fallback to the default)
            returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();

            return String.IsNullOrEmpty(returnUrl) ? DefaultRedirectAction() : ReturnRedirect(returnUrl);
        }

        private bool IsValidMVCPageRoute(string url)
        {
            //!!! Not for production
            string[] allowedRoutePrefixes = {
            "/Attendees",
            "/Home",
            "/Characters",
            "/ViewComponentMVCPage"
            // Add your specific route prefixes here
            };

            return allowedRoutePrefixes.Any(prefix =>
                url.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
        }

        private Dictionary<string, List<string>> GetModelState(ModelStateDictionary ModelState)
        {
            return ModelState
                       .Where(ms => ms.Value.Errors.Count > 0)
                       .ToDictionary(
                           kvp => kvp.Key,
                           kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                       );
        }
    }

}
