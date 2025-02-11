using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.T_Entities;
using Package.Shared.Services.ComponentServices;
using Package.Shared.Services.Configuration.CounterConfiguration;
using System.Net.Http.Json;
using Serilog; //qqqq i hate i am sepcifying the logger in the package



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

        public DateTime ServiceCreatedAt { get; set; }

        //could gate these with whoami
        public string CountInWASMService { get; set; } = "0";
        public string CountInServerService { get; set; } = "0";
        public string ServiceIdentifier { get; set; }

        private readonly ILogger<T_StateCounterTestService> _logger;
        private readonly Serilog.ILogger _serilogger;
        
        public T_StateCounterTestService(IHttpClientFactory httpClientFactory, IOptions<T_CounterAPIConfiguration> t_CounterAPIConfiguration, ILocalStorageService localStorage, IGS_JSEnabled gS_JSEnabled, ILogger<T_StateCounterTestService> logger) 
        {
            _logger = logger;
            _serilogger = Log.ForContext<T_StateCounterTestService>(); // Create a logger 
            ServiceIdentifier = "Service_" + Guid.NewGuid().ToString();

            GS_JSEnabled = gS_JSEnabled;

            _counterAPIConfiguration = t_CounterAPIConfiguration.Value;
            _counterAPIEndpoints = _counterAPIConfiguration.Endpoints.Counter;

   
            _http = httpClientFactory.CreateClient(_counterAPIConfiguration.ClientName);

            ServiceCreatedAt = DateTime.UtcNow;

            _localStorage = localStorage;

            _localStorage.SetItemAsync($"{DateTime.Now.ToString("yyyyMMdd__HH_mm_ss_ffff")}_initialised_service", ServiceIdentifier);
            Console.WriteLine(ServiceIdentifier + "_Initialised");
        }
  
        public string WhoMadeMe { get { return GS_JSEnabled.TestingWhoAmI; } }
        public string GetCountFromWASMService()
        {
            var debuginABC = WhoMadeMe;
            return CountInWASMService;
        }

        public string SetCountInWASMService(string count)
        {
            var debuginABC = WhoMadeMe;
            CountInWASMService = count;
            CountInWASMServiceChanged?.Invoke();
            return CountInWASMService; // leaving in encase needed for nojs
        }

        public string GetCountFromServerService()
        {
            var debuginABC = WhoMadeMe;
            return CountInServerService;
        }

        public string SetCountInServerService(string count)
        {
            var debuginABC = WhoMadeMe;
            CountInServerService = count;
            return CountInServerService; // leaving in encase needed for nojs
        }

        public string IncrementCountInWASMService()
        {
            var debuginABC = WhoMadeMe;
            string countStr = GetCountFromWASMService();
            int count = Int32.Parse(countStr);
            count++;
            SetCountInWASMService(count.ToString());
            return GetCountFromWASMService();
        }

        public string IncrementCountInServerService()
        {
            var debuginABC = WhoMadeMe;
            string countStr = GetCountFromServerService();
            int count = Int32.Parse(countStr);
            count++;
            SetCountInServerService(count.ToString());
            return GetCountFromServerService();
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
            return await GetCountFromStorage();// not really needed but will include as i think this is a pattern we may require to enable controllers to share nojs functionality
        }
        public async Task<string> IncrementCountInStorage()
        {
            string countStr = await GetCountFromStorage();
            int count = Int32.Parse(countStr);
            count++;
            await SetCountInStorage(count.ToString());
            return await GetCountFromStorage();
        }

        public class WeatherForecast
        {
            // The date for the weather forecast
            public DateTime Date { get; set; }

            // Temperature in Celsius
            public int TemperatureC { get; set; }

            // Temperature in Fahrenheit (computed property)
            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

            // Weather summary (e.g., "Sunny", "Rainy", etc.)
            public string Summary { get; set; }
        }

        public async Task<string> GetCountFromDB()
        {

        
            var result = (await _http.GetFromJsonAsync<GE_ServiceResponse<string>>($"{_http.BaseAddress}{_counterAPIEndpoints.GetCountFromDB}"));
        
            var count = result.Data ?? "0";
  
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
        //  put dispose on the services - if multiple components all dispose will get multiple entries 
        public async Task OnDisposeSaveObjectToStorage(string count, string countType, string userName, string typeDisposed, string individualIdentifierForDisposed)
        {
            Console.WriteLine("OnDisposeSaveObjectToStorage");
            var userStorage = new T_LocalStorage.UserStorageClass(typeDisposed, individualIdentifierForDisposed, ServiceIdentifier/*userName*/, count, countType);
            //await Task.Delay(5000);//Yep concurrency issue --- Even so 2 not 3 often finish, suggesting the service is lost before it is finished and when it is is lost its dispose is not called 
            try
            {
                await _localStorage.SetItemAsync($"{DateTime.Now.ToString("yyyyMMdd__HH_mm_ss_ffff")}_InsertedByServiceMethod", individualIdentifierForDisposed ?? "Unknown");
                //Console.WriteLine(WhoMadeMe + "  " + "OnDisposeSaveObjectToStorage");


                // Create a new UserStorageClass



                // Retrieve the existing data from localStorage, or initialize a new T_LocalStorage if it's not present
                T_LocalStorage existingData = await _localStorage.GetItemAsync<T_LocalStorage>("userData") ?? new T_LocalStorage();

                // Add the new user entry to the existing data
                existingData.Users.Add(userStorage);

                // Save the updated data back to localStorage
                await _localStorage.SetItemAsync("userData", existingData);
                await _localStorage.SetItemAsync($"{DateTime.Now.ToString("yyyyMMdd__HH_mm_ss_ffff")}_EndCallService_OnDisposeSaveObjectToStorage", "aflkh");
                    // Save the updated data back to localStorage
            }
            catch (Exception ex)
            {
                var debuginABC = WhoMadeMe;
            }
         

        }

        public async ValueTask DisposeAsync()
        {
            await _localStorage.SetItemAsync($"{DateTime.Now.ToString("yyyyMMdd__HH_mm_ss_ffff")}_InsertedByService_x_DISPOSEASYNC_x_Method", ServiceIdentifier);
            // Perform cleanup
            //await Task.Delay(1000); // Simulate some async cleanup work


            //Currently just testing when disposal happening later will post to storage
            var debuginABC = WhoMadeMe;
            try
            {
                Console.WriteLine("MyService constructed");
                //Console.WriteLine("Hi");
                //Console.WriteLine(WhoMadeMe + "  " + "StateServiceDisposed");
                await OnDisposeSaveObjectToStorage(count: "999", countType: "placeholder_countType", userName: "placeholder_username", typeDisposed: "Service_x_" + WhoMadeMe, individualIdentifierForDisposed: ServiceIdentifier);
            }
            catch (Exception e)
            {

                //Server side cant write to browser console
                var whomad = WhoMadeMe;
                var x = 1;
            }
           
            //return ValueTask.CompletedTask;
        }

        //public void Dispose()
        //{
        //    Console.WriteLine("Should be using async disposable but i am dispose and i am called if you see this");
        //    _localStorage.SetItemAsync($"{DateTime.Now.ToString("yyyyMMdd__HH_mm_ss_ffff")}_Shouldnt_use-dispose-without-async", ServiceIdentifier);
        //}
    }
}
