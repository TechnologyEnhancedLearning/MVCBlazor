using LH.DB.API.Data;
using Package.LH.Entities.Models;
using Package.LH.Services.Interfaces;
using Package.Shared.Entities.Communication;
using System.Linq;

namespace LH.DB.API.Services
{
    public class LH_AttendeesDBService : ILHS_AttendeesDbService
    {
        private readonly ISimulatedDatabase _database;
        private readonly List<LH_AttendeeModel> _attendees;
        public LH_AttendeesDBService(ISimulatedDatabase database)
        {
            _database = database;
            //For the current scope just want a list
            _attendees = _database.Meetings.First().People;
            Console.WriteLine("AttendeeDbService : Constructor");
        }

        // Fetch all attendees from the simulated database
        public async Task<GE_ServiceResponse<List<LH_AttendeeModel>>> LoadAttendeesAsync()
        {
            Console.WriteLine("AttendeeDbService : LoadAttendeesAsync");
            // Return a copy of the list to avoid external modifications
            return new GE_ServiceResponse<List<LH_AttendeeModel>>{ Data = _attendees};//If real db then would really be async
        }

        // Replace the entire list of attendees in the simulated database
        public async Task<GE_ServiceResponse<List<LH_AttendeeModel>>> ReplaceDBWithList(List<LH_AttendeeModel> attendees) //qqqq warning!!! for now just first group
        {

            // Replace the current list with the new list
            _attendees.Clear();  // Clear existing attendees
            _attendees.AddRange(attendees);  // Add the new attendees
            Console.WriteLine("AttendeeDbService : ReplaceDBWithList");
            return new GE_ServiceResponse<List<LH_AttendeeModel>> { Data = _attendees };
        }


        public async Task<GE_ServiceResponse<bool>> RemoveAttendeeByTemporaryId_NoJS(Guid ClientTemporaryId)
        {

            try
            {
                LH_AttendeeModel attendeeToRemove = _attendees.Single(x => x.ClientTemporaryId == ClientTemporaryId);

                _attendees.Remove(attendeeToRemove);
                //await _database.SaveChangesAsync(); isnt a real  db

                return new GE_ServiceResponse<bool>{ Data = true} ;

            }
            catch (Exception e)
            {
                //Not single, not first, not found - do something here
                throw;
            }

        }


    }
}
