using Blazored.LocalStorage;
using Microsoft.Extensions.Options;
using Package.Shared.Entities.Communication;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.Configuration.CounterConfiguration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.StateServices.T_Services
{
    public class T_StateCounterTestService : IT_StateCounterTestService
    {
        public readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        public IGS_JSEnabled GS_JSEnabled { get; set; }
        private T_CounterAPIConfiguration _counterAPIConfiguration;
        private IT_CounterAPIEndpoints _counterAPIEndpoints;

        public event Action CountInDBChanged;
        public event Action CountInStorageChanged;
        public event Action CountInWASMServiceChanged;
        //qqqqq need to do all the wasm ones
        public DateTime ServiceCreatedAt { get; set; }

        //qqqqqq gate these with whoami
        public string CountInWASMService { get; set; } = "0";
        public string CountInServerService { get; set; } = "0";
        public T_StateCounterTestService(IHttpClientFactory httpClientFactory, IOptions<T_CounterAPIConfiguration> t_CounterAPIConfiguration, ILocalStorageService localStorage, IGS_JSEnabled gS_JSEnabled) 
        {
            GS_JSEnabled = gS_JSEnabled;

            _counterAPIConfiguration = t_CounterAPIConfiguration.Value;
            _counterAPIEndpoints = _counterAPIConfiguration.Endpoints.Counter;

            //Debug.WriteLine(_attendeesAPIConfiguration.ClientName);//qqqqqq put in logger
            _http = httpClientFactory.CreateClient(_counterAPIConfiguration.ClientName);

            ServiceCreatedAt = DateTime.UtcNow;

            _localStorage = localStorage;
        }
  
        public string WhoMadeMe { get { return GS_JSEnabled.TestingWhoAmI; } }
        public string GetCountFromWASMService()
        {
            return CountInWASMService;
        }

        public string SetCountInWASMService(string count)
        {
            CountInWASMService = count;
            CountInWASMServiceChanged?.Invoke();
            return CountInWASMService; // leaving in encase needed for nojs
        }

        public string IncrementCountInWASMService()
        {

            string countStr = GetCountFromWASMService();
            int count = Int32.Parse(countStr);
            count++;
            SetCountInWASMService(count.ToString());
            return GetCountFromWASMService();
        }

        public async Task<string> GetCountFromStorage()
        {
            var storageResult = await _localStorage.GetItemAsync<string>("CountInLocalStorage");
            return storageResult ?? "0";
        }

        public async Task<string> SetCountInStorage(string count)
        {
            await _localStorage.SetItemAsync("CountInLocalStorage", count);
            CountInStorageChanged?.Invoke();
            return await GetCountFromStorage();//qqqq not really needed but will include as i think this is a pattern we may require to enable controllers to share nojs functionality
        }
        public async Task<string> IncrementCountInStorage()
        {
            string countStr = await GetCountFromStorage();
            int count = Int32.Parse(countStr);
            count++;
            await SetCountInStorage(count.ToString());
            return await GetCountFromStorage();
        }



        public async Task<string> GetCountFromDB()
        {
            var count = (await _http.GetFromJsonAsync<GE_ServiceResponse<string>>($"{_http.BaseAddress}{_counterAPIEndpoints.GetCountFromDB}")).Data ?? "0";
            return count;
        }

        public async Task<string> SetCountInDB(string count)
        {
            //we do pass it back anyway but as its an example proj
            var response = await _http.PostAsJsonAsync($"{_http.BaseAddress}{_counterAPIEndpoints.SetCountInDB}", count);
            var result = await response.Content.ReadFromJsonAsync<GE_ServiceResponse<string>>();
            CountInDBChanged?.Invoke();
            return result?.Data ?? "0";
        }

        public async Task<string> IncrementCountInDB()
        {
            string countStr = await GetCountFromDB();
            int count = Int32.Parse(countStr);
            count++;
            await SetCountInDB(count.ToString());

            return await GetCountFromDB();
        }

    }
}
