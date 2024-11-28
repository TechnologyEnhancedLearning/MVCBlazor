using Microsoft.Extensions.Configuration;
using Package.LH.Entities.Models;
using Package.Shared.Entities.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Interfaces
{
    public interface ILHS_AttendeesDbService 
    {
        public Task<GE_ServiceResponse<List<LH_AttendeeModel>>> LoadAttendeesAsync();
        public Task<GE_ServiceResponse<List<LH_AttendeeModel>>> ReplaceDBWithList(List<LH_AttendeeModel> attendees);
        public Task<GE_ServiceResponse<bool>> RemoveAttendeeByTemporaryId_NoJS(Guid ClientTemporaryId);
    }
}
