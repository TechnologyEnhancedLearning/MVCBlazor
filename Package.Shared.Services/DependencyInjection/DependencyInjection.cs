using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Package.Shared.Services.Configurations;
using Package.Shared.Services.StateServices.CharacterStateServices;
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
        // qqqq we should inject everything required to run the package - this is true of all the dependencys
        public static IServiceCollection GS_AddStateServices(this IServiceCollection services)
        {

            //Add Services
            services.AddScoped<IGS_CharactersStateService, GS_CharactersStateService>();
            return services;
        }

        public static IServiceCollection GS_AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            //Add Configuration
            services.Configure<GS_CharactersAPIEndpoints>(configuration.GetSection("APIEndpoints:Characters"));
            return services;
        }
    }
}
