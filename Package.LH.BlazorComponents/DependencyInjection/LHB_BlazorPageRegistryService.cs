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
                "/Attendees/Static-BlazorPage",
                "/Attendees/InteractiveAuto-BlazorPage",
                "/Attendees/InteractiveAutoPrerenderedFalseWeatherPage-BlazorPage",
                "/Attendees/InteractiveServer-BlazorPage",
                "/Attendees/InteractiveServerPrendereedFalse-BlazorPage",
                "/Attendees/StaticServer-BlazorPage",
                "/Attendees/InteractiveWebAssembly-BlazorPage",
                "/Attendees/InteractiveWebAssemblyPrerenderedFalse-BlazorPage",

                "/Characters/InteractiveAuto-BlazorPage",
                "/Characters/InteractiveAutoPrerenderFalseWeatherPage-BlazorPage",
                "/Characters/InteractiveServerPage-BlazorPage",
                "/Characters/InteractiveServerPrenderFalse-BlazorPage",
                "/Characters/StaticServerPage-BlazorPage",
                "/Characters/InteractiveWebAssembly-BlazorPage",
                "/Characters/InteractiveWebAssemblyPrerenderedFalse-BlazorPage",

                "/StreamRenderingWeather-BlazorPage"










            };
        }
    }
}
