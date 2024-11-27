using Package.LH.Entities.Models;
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
        Task<List<LH_AttendeeModel>> GetAttendeesAsync();

        Task AddAttendeeAsync(LH_AttendeeModel attendee);

        Task RemoveAttendeeByTemporaryIdAsync(Guid clientTemporaryId);

        Task<List<LH_AttendeeModel>> ReplaceDBWithListAsync();

        bool DataIsLoaded { get; }

        List<LH_AttendeeModel> Attendees { get;  }


    }
}
