using Package.LH.Entities.Models;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.StateServices
{
    public interface ILHS_AttendeesStateService
    {
        Task EnsureDataIsLoadedAsync();
        Task<GE_ServiceResponse<List<LH_AttendeeModel>>> GetAttendeesAsync();

        Task<GE_ServiceResponse<bool>> AddAttendeeAsync(LH_AttendeeModel attendee);

        Task<GE_ServiceResponse<bool>> RemoveAttendeeByTemporaryIdAsync(Guid clientTemporaryId);

        Task<GE_ServiceResponse<List<LH_AttendeeModel>>> ReplaceDBWithListAsync();

        bool DataIsLoaded { get; }

        List<LH_AttendeeModel> Attendees { get;  }


    }
}
