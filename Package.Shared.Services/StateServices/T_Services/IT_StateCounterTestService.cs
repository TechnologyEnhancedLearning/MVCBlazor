using Blazored.LocalStorage;
using Package.Shared.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.StateServices.T_Services
{
    // QQQQ This interface may need seperating into parts. Ultimately the prerender wont use localstorage,
    // and we wont be injecting the services that are used in the constructor of the implementation so seperate implementations would be needed
    // this also suggests if storage is common as would expect for components they have their own implementation
    public interface IT_StateCounterTestService
    {
        //For testing purposes only, these wouldnt all be public or necessary
        
        public string CountInWASMService { get; set; }
        public DateTime ServiceCreatedAt { get; set; }
        public event Action CountInDBChanged, 
                            CountInStorageChanged, 
                            CountInWASMServiceChanged;

        public string WhoMadeMe { get; }

        // Local Storage

        public Task<string> GetCountFromStorage();
        public Task<string> SetCountInStorage(string count);
        public Task<string> IncrementCountInStorage();

        // Session ?? QQQQ ??
        /*QQQQ in future maybe in service too*/

        // DB

        public Task<string> GetCountFromDB();
        public Task<string> SetCountInDB(string count);
        public Task<string> IncrementCountInDB();

        // Service

        public string GetCountFromWASMService();
        public string SetCountInWASMService(string count);
        public string IncrementCountInWASMService();

    





    }
}
