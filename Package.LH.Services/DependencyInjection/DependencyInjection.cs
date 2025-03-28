﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Package.LH.Services.StateServices;
using Package.LH.Services.Configurations.AttendeesConfiguration;
using Package.LH.Services.Configurations;

namespace Package.LH.Services.DependencyInjection
{
    public static class DependencyInjection
    {

        public static IServiceCollection LHS_AddStateServices(this IServiceCollection services)
        {
            services.AddScoped<ILHS_AttendeesStateService, LHS_AttendeesStateService>();
            return services;
        }
        public static IServiceCollection LHS_AddConfiguration(this IServiceCollection services, IConfiguration configuration, string apiSection)
        {

            //Add Configuration
            services.Configure<LHS_AttendeesAPIConfiguration>(configuration.GetSection(apiSection));

            return services;
        }
    }
}

