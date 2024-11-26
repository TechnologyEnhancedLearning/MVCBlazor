using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.Enums
{
    public enum GB_ButtonType
    {
        Generic,
        Danger,
        Success,
        Info
    }
    public enum GB_ComponentTagRenderMode
    { 
        Static, //in blazor pages serverStatic
        Server, //in blazor pages InteractiveServer
        ServerPrendered,
        WebAssembly,
        WebAssemblyPrerendered
        //InteractiveAuto
        //InteractivePrerenderFalse
        /*Auto interactive and autointeractive prerender false and streamrendering not available to mvc components*/
    
    }
    public static class GB_ButtonTypeExtensions
    {
        public static string GetButtonClass(this GB_ButtonType type)
        {
            return type switch
            {
                GB_ButtonType.Danger => "nhsuk-danger-button",
                GB_ButtonType.Success => "nhsuk-success-button",
                GB_ButtonType.Info => "nhsuk-info-button",

                _ => "nhsuk-generic-button" // Default to Generic button if not specified
            };
        }
    }
}
