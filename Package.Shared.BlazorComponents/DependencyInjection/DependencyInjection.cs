using Blazored.LocalStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Package.Shared.BlazorComponents.BaseComponents.Buttons;
using Package.Shared.BlazorComponents.BaseComponents.EditForm;
using Package.Shared.BlazorComponents.BaseComponents.Validation;
using Package.Shared.BlazorComponents.Components.Buttons;
using Package.Shared.BlazorComponents.Components.Lists;
using Package.Shared.Entities.Interfaces.ComponentInterfaces;
using Package.Shared.Services.Configuration.CharactersConfiguration;
using Package.Shared.Services.Configuration.CounterConfiguration;
using Package.Shared.Services.StateServices.CharacterStateServices;
using Package.Shared.Services.StateServices.T_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.DependencyInjection
{
    public static class DependencyInjection
    {
        // qqqq i want logger here so we get an error when things not provided to the collection on update
        //public static IServiceCollection GB_ComponentServices(this IServiceCollection services,  <ILogger> logger)//qqqq can i have a default
        //{
        //    services.AddScoped<ILogger, logger>();//qqqq but for blazor this could be singleton, serverside it shouldnt be
        //    return services;
        //}
        //qqqq loook at later just make work for now - automatic for serilog and ilogger interface
        //public static IServiceCollection GB_AddConfiguration(this IServiceCollection services, IConfiguration configuration, string loggerSection)
        //{

        //    //Add Configuration
        //    services.Configure<GS_CharactersAPIConfiguration>(configuration.GetSection(loggerSection));
        //    return services;
        //}
    }
}
