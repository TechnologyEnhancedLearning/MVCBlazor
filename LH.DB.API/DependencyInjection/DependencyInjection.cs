using LH.DB.API.Services;
using Package.LH.Services.Interfaces;
using Package.Shared.Services.Interfaces;

namespace LH.DB.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection LHAPI_AddDbServices(this IServiceCollection services)
        {
            //These are the default implementations
            services.AddSingleton<IT_CounterDBService, T_CounterDBService>();
            services.AddSingleton<ILHS_AttendeesDbService, LH_AttendeesDBService>();
            services.AddSingleton<IGS_CharactersDBService, LH_CharactersDBService>();
            services.AddSingleton<T_LoggerDBService>();
            return services;
        }
    }
}
