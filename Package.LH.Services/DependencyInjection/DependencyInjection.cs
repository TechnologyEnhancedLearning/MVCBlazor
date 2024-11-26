using Package.LH.Services.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Package.Shared.Services.Configurations;
using Package.LH.Services.StateServices.LHS_AttendeesStateServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.DependencyInjection
{
    public static class DependencyInjection
    {

            public static IServiceCollection LHS_AddStateServices(this IServiceCollection services)
            {
                services.AddScoped<ILHS_AttendeeStateService, LHS_AttendeeStateService>();
                return services;
            }
            public static IServiceCollection LHS_AddConfiguration(this IServiceCollection services, IConfiguration configuration)
            {

                //Add Configuration
                //qqqq here we force appsetting layout so dont need to do through interfaces as before, 
                services.Configure<LHS_AttendeeApiEndPoints>(configuration.GetSection("ApiEndpoints:Attendees"));

                return services;
            }
        }

