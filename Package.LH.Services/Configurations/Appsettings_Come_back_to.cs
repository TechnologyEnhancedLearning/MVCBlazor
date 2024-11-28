using Package.LH.Services.Configurations;
using Package.Shared.Entities.Configuration;

namespace Package.LH.Services.Configurations
{
    public interface Appsettings_Come_back_to : IAppsettings_Come_back_to
    {

        IAPIsConfiguration APIs { get; set; }
        ILoggingConfiguration Logging { get; set; }
        string AllowedHosts { get; set; }


        // APIs Configuration
        public interface IAPIsConfiguration
        {
            ILH_DB_API LH_DB_API { get; set; }
        }
        public interface ILH_DB_API : ILHS_Configuration //if we want to add others configuration to the same api we do it here
        { }


        // Logging Configuration
        public interface ILoggingConfiguration
        {
            ILogLevelConfiguration LogLevel { get; set; }
        }

        public interface ILogLevelConfiguration
        {
            string Default { get; set; }
            string MicrosoftAspNetCore { get; set; }
        }

    }
}
