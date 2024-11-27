using Package.LH.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Interfaces
{
    public interface ILHS_AttendeesDbService
    {
        public Task<List<LH_AttendeeModel>> LoadAttendeesAsync();
        public Task ReplaceDBWithList(List<LH_AttendeeModel> attendees);
        public Task<List<LH_AttendeeModel>> RemoveAttendeeByTemporaryId_NoJS(Guid ClientTemporaryId);
    }
}
