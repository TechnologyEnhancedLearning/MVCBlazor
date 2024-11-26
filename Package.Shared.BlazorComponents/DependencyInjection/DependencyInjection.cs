using Microsoft.Extensions.DependencyInjection;
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
            services.AddTransient(typeof(RadioListVCB<,>));
            services.AddTransient(typeof(ValidatorVCB<IBlazorModelStateValidationErrors>));
            services.AddTransient<RadioList_TestSpace>();
            services.AddTransient<TestBlazor_VCB>();

            //Components can be transcient its the service that may need scope (i think)
            services.AddTransient<GenericButton>();
            services.AddTransient<GenericButton_Blazor>();
            services.AddTransient<GenericButton_NoJS>();

            services.AddTransient<WriteLineButton>();
            // services.AddTransient<ListWithButtons<TItem>>();
            services.AddTransient(typeof(ListWithButtons<>));//qqqq notice we need type of because takes a generic (TItem)
            return services;
        }




    }
}
