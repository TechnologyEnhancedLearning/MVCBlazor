using Serilog.Core;
using Serilog.Events;

namespace Package.Shared.Services.HelperServices.LogLevelSwitcherService
{
    public interface ILogLevelSwitcherService
    {
        //    public event Action OnLogLevelChanged; //Won't do this too many events so will just says its a slow process


        /// <summary>
        /// String so can be more generic
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLogLevel();

        public string SetLogLevel(string level);

        public List<string> GetAvailableLogLevels();
        public bool IsInitialized { get; set; }
        public Task InitializeLogLevelFromAsyncSourceIfAvailable();
        //constructor cant call a db or local storage because async so needs doing in async
        //So would need to be in the base component async constructor - seems expensive
    }
}