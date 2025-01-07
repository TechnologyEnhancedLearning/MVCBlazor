using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Package.Shared.BlazorComponents.UnitTests.TestDoubles;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.StateServices.CharacterStateServices;
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
        public static IServiceCollection AddTestDouble_GS_StateServices(this IServiceCollection services)
        {
            /*Mocks, Stubs, Fakes here*/

            FakeGS_CharactersStateService FakeGS_CharactersStateService = new FakeGS_CharactersStateService();
            services.AddScoped<IGS_CharactersStateService, FakeGS_CharactersStateService>(serviceProvider => FakeGS_CharactersStateService);

            return services;
        }

    }  
}

