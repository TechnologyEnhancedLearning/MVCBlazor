using Package.LH.BlazorComponents.UnitTests.TestDoubles;
using Package.LH.Services.StateServices;
using Package.Shared.Services.StateServices.CharacterStateServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.BlazorComponents.UnitTests.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTestDouble_LHS_AttendeesStateServices(this IServiceCollection services)
        {
            /*Mocks, Stubs, Fakes here*/

            FAKE_LHS_AttendeesStateServices FAKE_LHS_AttendeesStateServices = new FAKE_LHS_AttendeesStateServices();
            services.AddScoped<ILHS_AttendeesStateService, FAKE_LHS_AttendeesStateServices>(serviceProvider => FAKE_LHS_AttendeesStateServices);

            return services;
        }


    
    }
}
