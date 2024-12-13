using Microsoft.Extensions.DependencyInjection;
using Package.LH.BlazorComponents.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
//using Package.LH.BlazorComponents.Components.Pages.SharedTestingPageComponents;

namespace Package.LH.BlazorComponents.DependencyInjection
{
    public static class DependencyInjection
    {

        public static IServiceCollection LHB_RegisterAllBlazorPageRoutes(this IServiceCollection services)
        {

            //Components can be transcient its the service that may need scope (i think)
            services.AddSingleton<LHB_BlazorPageRegistryService>();
            return services;
        }
    }
}
