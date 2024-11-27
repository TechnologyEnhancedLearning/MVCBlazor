using Microsoft.Extensions.DependencyInjection;
using Package.Shared.BlazorComponents.BaseComponents.Buttons;
using Package.Shared.BlazorComponents.BaseComponents.Validation;
using Package.Shared.BlazorComponents.Components.Buttons;
using Package.Shared.BlazorComponents.Components.Lists;
using Package.Shared.BlazorComponents.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection GB_RegisterAllGenericBlazorComponents(this IServiceCollection services/*, qqqq IOptions<ILHBlazorWasmClientServiceLibraryConfiguration> config*/)
        {
            //services.AddTransient(typeof(RadioListVCB<,>));
            services.AddTransient(typeof(GB_Validator<IGB_ModelStateValidation>));
            //services.AddTransient<RadioList_TestSpace>();
            //services.AddTransient<TestBlazor_VCB>();

            //Components can be transcient its the service that may need scope (i think)
            services.AddTransient<GB_Button>();
            services.AddTransient<GB_Button_I>();
            services.AddTransient<GB_Button_S>();

            services.AddTransient<GB_WriteLineButton>();
            // services.AddTransient<ListWithButtons<TItem>>();
            services.AddTransient(typeof(GB_ListWithButtons<>));//qqqq notice we need type of because takes a generic (TItem)
            return services;
        }




    }
}
