using Package.Shared.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Interfaces
{
    public interface IT_CounterDBService
    {
        public Task<GE_ServiceResponse<string>> SetCountInDB(string count);
        public Task<GE_ServiceResponse<string>> GetCountFromDB();
    }
}
