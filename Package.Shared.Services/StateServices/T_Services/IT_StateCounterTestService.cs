using Package.Shared.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.StateServices.T_Services
{
    public interface IT_StateCounterTestService
    {
        //For testing purposes only, these wouldnt all be public or necessary
        public string SaveCountToDB { get; set; }


        public event Action CountInDBChanged, CountInStorageChanged;
        public string WhoMadeMe { get; }
        public string GetStorage { get; set; }
        public string GetCountFromStorage { get; set; }
        public string SetCountInStorage { get; set; }
        public string GetCountFromDB { get;set; }
        public string GetCountFromService { get; set; }
        public string SetCountForService { get; set; }
        public string IncrementCount { get; set; }


    }
}
