namespace LH.DB.API.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbServicesFromLHAPIPackage(this IServiceCollection services)
        {
            //These are the default implementations
            services.AddSingleton<IAttendeesDbService, AttendeesDbService>();
            services.AddSingleton<ICharactersDbService, CharactersDbService>();
            return services;
        }
    }
}
