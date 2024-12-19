using Microsoft.Extensions.DependencyInjection;
using Package.Shared.BlazorComponents.BaseComponents.Buttons;
using Package.Shared.BlazorComponents.BaseComponents.EditForm;
using Package.Shared.BlazorComponents.BaseComponents.Validation;
using Package.Shared.BlazorComponents.Components.Buttons;
using Package.Shared.BlazorComponents.Components.Lists;
using Package.Shared.Entities.Interfaces.ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.DependencyInjection
{
    public static class DependencyInjection
    {
        //public static IServiceCollection GB_RegisterAllGenericBlazorComponents(this IServiceCollection services/*,  IOptions<ILHBlazorWasmClientServiceLibraryConfiguration> config*/)
        //{
        //    services.AddTransient(typeof(GB_Validator<IGE_ModelStateValidation>));
        //    services.AddTransient(typeof(GB_EditForm<IGE_ModelStateValidation>));


        //    //Components can be transcient its the service that may need scope (i think)
        //    services.AddTransient<GB_Button>();
        //    services.AddTransient<GB_Button_I>();
        //    services.AddTransient<GB_Button_S>();
        //    services.AddTransient<GB_Button_Submit>();

        //    services.AddTransient<GB_WriteLineButton>();
        //    services.AddTransient(typeof(GB_ListWithButtons<>));//we need type of because takes a generic (TItem)
        //    return services;
        //}




    }
}
