using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Package.Shared.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.StateServices.T_Services
{
    //  This interface may need seperating into parts. Ultimately the prerender wont use localstorage,
    // and we wont be injecting the services that are used in the constructor of the implementation so seperate implementations would be needed
    // this also suggests if storage is common as would expect for components they have their own implementation
    public interface IT_StateCounterTestService :IAsyncDisposable //, IDisposable //idisposable on this is bad practice currenlty just for testing it should be async 
    {
        
        //For testing purposes only, these wouldnt all be public or necessary
        public string ServiceIdentifier { get; set; }
        public string CountInWASMService { get; set; }
        public string CountInServerService { get; set; }
        public DateTime ServiceCreatedAt { get; set; }
        public event Action CountInDBChanged, 
                            CountInStorageChanged, 
                            CountInWASMServiceChanged;

        public string WhoMadeMe { get; }

        // Local Storage

        public Task<string> GetCountFromStorage();
        public Task<string> SetCountInStorage(string count);
        public Task<string> IncrementCountInStorage();



        public Task<string> GetCountFromDB();
        public Task<string> SetCountInDB(string count);
        public Task<string> IncrementCountInDB();

        // Service

        public string GetCountFromWASMService();
        public string SetCountInWASMService(string count);
        public string IncrementCountInWASMService();

        public string GetCountFromServerService();
        public string SetCountInServerService(string count);
        public string IncrementCountInServerService();



        public Task OnDisposeSaveObjectToStorage(string count, string countType, string userName, string typeDisposed, string individualIdentifierForDisposed);



    }
}
