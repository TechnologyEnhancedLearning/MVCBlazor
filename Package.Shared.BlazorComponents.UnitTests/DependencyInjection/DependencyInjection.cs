using Microsoft.Extensions.DependencyInjection;
using Package.Shared.Services.ComponentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.UnitTests.DependencyInjection
{
    /// <summary>
    /// We will want mocked service (generated empty)
    /// Dummy services (custom replacements)
    /// qqqq remember one service overwrite another so can set nojs after
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection OverrideJsEnabledToTrue(this IServiceCollection services)
        {
            //Qqqq
            services.AddScoped<IGS_JSEnabled>(s => new GS_JSEnabled{ JSIsEnabled = true});
            return services;
        }
    }



    
}


//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Package.LH.Services.StateServices;
//using Package.LH.Services.Configurations.AttendeesConfiguration;
//using Package.LH.Services.Configurations;

//namespace Package.LH.Services.DependencyInjection
//{
//    public static class DependencyInjection
//    {

//        public static IServiceCollection LHS_AddStateServices(this IServiceCollection services)
//        {

//        }
//        public static IServiceCollection LHS_AddConfiguration(this IServiceCollection services, IConfiguration configuration, string apiSection)
//        {


//            return services;
//        }
//    }
//}


