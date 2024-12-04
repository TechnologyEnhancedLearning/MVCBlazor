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

        /// <summary>
        /// To prevent tree shaking
        /// To enable package update to not require client code update for adding components
        /// Should contain all or all components not directly references in client wasm, and all component directly referenced in server (though server doesnt need this as it can get directly from package as it is referencing it directly)
        ///m qqqq in actuality the server may never need this only the client
        ///        - !!! they are not really services so ultimately i dont like this !!!

        //we do this so we can prevent tree shaking of components consumed by the server asking the wasm to produce them from its package reference

        //Reflection of the components folder would mean we dont forget to register
        // */
        // qqqq try replacing with import assemblies
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection LHB_RegisterAllBlazorComponents(this IServiceCollection services)
        {

            services.AddTransient<LHB_Attendees_AddRemoveListForm>();
            services.AddTransient<LHB_FavouriteCharacterForm>();
    


            //services.AddTransient<CharactersPageComponent>();
            //services.AddTransient<RenderModeDisplayer>();
            return services;
        }
        public static IServiceCollection LHB_RegisterAllBlazorPageRoutes(this IServiceCollection services)
        {



            //Components can be transcient its the service that may need scope (i think)
            services.AddSingleton<LHB_BlazorPageRegistryService>();
            return services;
        }
    }
}
