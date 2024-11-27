using Microsoft.AspNetCore.Mvc;
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

            protected LHB_BlazorPageRegistryService BlazorPageRegistryService  { get; }

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
                                                                   .Replace("http://", "");
                //.Replace("localhost:44371","");

                if (Url.IsLocalUrl(trimmedReturnUrlNotForProduction) //MVC Routes
                    || BlazorPageRegistryService.BlazorPageRoutes.Contains(trimmedReturnUrlNotForProduction.Replace("localhost:44371", "")) //Blazor page routes
                    )
                {
                    return Redirect(returnUrl); // Safe redirect
                                                //It would be nice if this was a controller and action
                                                // RedirectToAction
                }


                // If return URL is not local, redirect to the denied route
                return RedirectToAction(DeniedRouteController, DeniedRouteAction);
            }
        }
}
