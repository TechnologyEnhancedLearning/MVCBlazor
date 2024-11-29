using Package.Shared.BlazorComponents.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.BlazorComponents.DependencyInjection
{
    public class LHB_BlazorPageRegistryService
    {
        public HashSet<string> BlazorPageRoutes { get; }

        public LHB_BlazorPageRegistryService()
        {
            // Initialize the allowed routes list
            BlazorPageRoutes = new HashSet<string>
            {
                "/Attendees/InteractiveAutoPage-BlazorPage",
                "/Attendees/InteractiveAutoPrerenderFalseWeatherPage-BlazorPage",
                "/Attendees/InteractiveServerPage-BlazorPage",
                "/Attendees/InteractiveServerPagePrenderFalse-BlazorPage",
                "/Attendees/StaticServerPage-BlazorPage",
                "/Attendees/InteractiveWebAssemblyPage-BlazorPage",
                "/Attendees/InteractiveWebAssemblyPrerenderFalse-BlazorPage",

                "/Characters/InteractiveAutoPage-BlazorPage",
                "/Characters/InteractiveAutoPrerenderFalseWeatherPage-BlazorPage",
                "/Characters/InteractiveServerPage-BlazorPage",
                "/Characters/InteractiveServerPagePrenderFalse-BlazorPage",
                "/Characters/StaticServerPage-BlazorPage",
                "/Characters/InteractiveWebAssemblyPage-BlazorPage",
                "/Characters/InteractiveWebAssemblyPrerenderFalse-BlazorPage",

                "/StreamRenderingWeather-BlazorPage"










            };
        }
    }
}
