using Microsoft.Extensions.Options;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.Configuration.CounterConfiguration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.StateServices.T_Services
{
    public class T_StateCounterTestService : IT_StateCounterTestService
    {
        private readonly HttpClient _http;
        public IGS_JSEnabled GS_JSEnabled { get; set; }
        private T_CounterAPIConfiguration _counterAPIConfiguration;
        private IT_CounterAPIEndpoints _counterAPIEndpoints;
        public T_StateCounterTestService(IHttpClientFactory httpClientFactory, IOptions<T_CounterAPIConfiguration> t_CounterAPIConfiguration,  IGS_JSEnabled gS_JSEnabled) 
        {
            GS_JSEnabled = gS_JSEnabled;

            _counterAPIConfiguration = t_CounterAPIConfiguration.Value;
            _counterAPIEndpoints = _counterAPIConfiguration.Endpoints.Counter;

            //Debug.WriteLine(_attendeesAPIConfiguration.ClientName);//qqqqqq put in logger
            _http = httpClientFactory.CreateClient(_counterAPIConfiguration.ClientName);
        }
        public string SaveCountToDB { get ; set ; }
        public string WhoMadeMe { get { return GS_JSEnabled.TestingWhoAmI; } }
        public string GetStorage { get ; set ; }
        public string GetCountFromStorage { get ; set ; }
        public string SetCountInStorage { get ; set ; }
        public string GetCountFromDB { get ; set ; }
        public string GetCountFromService { get ; set ; }
        public string SetCountForService { get ; set ; }
        public string IncrementCount { get ; set ; }

        public event Action CountInDBChanged;
        public event Action CountInStorageChanged;
    }
}
