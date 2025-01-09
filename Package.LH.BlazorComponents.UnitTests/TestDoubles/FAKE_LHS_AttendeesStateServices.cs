using AutoFixture;
using Package.LH.Entities.Models;
using Package.LH.Services.StateServices;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.BlazorComponents.UnitTests.TestDoubles
{
    public class FAKE_LHS_AttendeesStateServices : ILHS_AttendeesStateService
    {

        private readonly List<LH_AttendeeModel> attendees;
        private readonly TaskCompletionSource<LH_AttendeeModel> attendeeAdded = new();

        public FAKE_LHS_AttendeesStateServices()
        {
            // Use AutoFixture to create test data
            var fixture = new Fixture();
            this.attendees = fixture.CreateMany<LH_AttendeeModel>(5).ToList(); 
        }

        public bool DataIsLoaded => true;

        public List<LH_AttendeeModel> Attendees => attendees;

        public Task<GE_ServiceResponse<bool>> AddAttendeeAsync(LH_AttendeeModel attendee)
        {

            attendees.Add(attendee);

            return Task.FromResult(new GE_ServiceResponse<bool> { Data = true, Success = true });
        }

        public Task EnsureDataIsLoadedAsync()
        {
            return Task.CompletedTask;
        }

        public Task<GE_ServiceResponse<List<LH_AttendeeModel>>> GetAttendeesAsync()
        {
            return Task.FromResult(new GE_ServiceResponse<List<LH_AttendeeModel>>()
            {
                Data = attendees,
                Success = true
            });
        }

        public Task<GE_ServiceResponse<bool>> RemoveAttendeeByTemporaryIdAsync(Guid clientTemporaryId)
        {
            var characterToRemove = attendees.Single(c => c.ClientTemporaryId == clientTemporaryId);
            if (characterToRemove != null)
            {
                attendees.Remove(characterToRemove);//qqqq is it too much logic
                return Task.FromResult(new GE_ServiceResponse<bool> { Data = true, Success = true });
            }

            return Task.FromResult(new GE_ServiceResponse<bool> { Data = false, Success = false });
        }

        public Task<GE_ServiceResponse<List<LH_AttendeeModel>>> ReplaceDBWithListAsync()
        {
            return Task.FromResult(new GE_ServiceResponse<List<LH_AttendeeModel>>()
            {
                Data = attendees,
                Success = true
            });
        }
    }
}
