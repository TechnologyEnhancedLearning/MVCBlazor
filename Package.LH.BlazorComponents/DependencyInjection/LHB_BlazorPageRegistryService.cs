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
                "/meeting-attendees",
                "/BlazorPage",
                "/CharactersBlazorPage",
                "/InteractiveAutoPage",
                "/InteractiveServerPage",
                "/InteractiveWebAssemblyPage",
                "/StaticServerPage",
                "/StreamRenderingWeatherPage",
                "/InteractiveAutoPrerenderFalseWeatherPage",

            };
        }
    }
}
