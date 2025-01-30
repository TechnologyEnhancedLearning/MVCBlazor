using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Package.Shared.Services.Configuration.CharactersConfiguration;
using Package.Shared.Services.Configuration.CounterConfiguration;
using Package.Shared.Services.StateServices.CharacterStateServices;
using Package.Shared.Services.StateServices.T_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.DependencyInjection
{
    public static class DependencyInjection
    {
        
        public static IServiceCollection GS_AddStateServices(this IServiceCollection services)
        {

            //Add Services
            services.AddScoped<IGS_CharactersStateService, GS_CharactersStateService>();
            services.AddScoped<IT_StateCounterTestService, T_StateCounterTestService>();
            return services;
        }

        public static IServiceCollection GS_AddConfiguration(this IServiceCollection services, IConfiguration configuration, string apiSection)
        {
            
            //Add Configuration
            services.Configure<GS_CharactersAPIConfiguration>(configuration.GetSection(apiSection));
            services.Configure<T_CounterAPIConfiguration>(configuration.GetSection(apiSection));
            return services;
        }
    }
}
