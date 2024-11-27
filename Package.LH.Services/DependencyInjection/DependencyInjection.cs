using Package.LH.Services.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Package.Shared.Services.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Package.LH.Services.StateServices;

namespace Package.LH.Services.DependencyInjection
{
    public static class DependencyInjection
    {

        public static IServiceCollection LHS_AddStateServices(this IServiceCollection services)
        {
            services.AddScoped<ILHS_AttendeesStateService, LHS_AttendeesStateService>();
            return services;
        }
        public static IServiceCollection LHS_AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            //Add Configuration
            services.Configure<LHS_AttendeesAPIEndpoints>(configuration.GetSection("APIEndpoints:Attendees"));

            return services;
        }
    }
}

