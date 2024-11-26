using LH.DB.API.Data;
using Package.LH.Entities.Models;

namespace LH.DB.API.Services
{
    public class LH_AttendeesDBService : IAttendeesDbService
    {
        private readonly ISimulatedDatabase _database;

        public LH_AttendeesDBService(ISimulatedDatabase database)
        {
            _database = database;
            Console.WriteLine("AttendeeDbService : Constructor");
        }

        // Fetch all attendees from the simulated database
        public Task<List<LH_AttendeeModel>> LoadAttendeesAsync()
        {
            Console.WriteLine("AttendeeDbService : LoadAttendeesAsync");
            // Return a copy of the list to avoid external modifications
            return Task.FromResult(_database.Meetings.FirstOrDefault().Attendees.ToList<LH_AttendeeModel>()); //qqqq warning!!! for now just first group
        }

        // Replace the entire list of attendees in the simulated database
        public Task ReplaceDbWithList(List<LH_AttendeeModel> attendees) //qqqq warning!!! for now just first group
        {
            // Replace the current list with the new list
            // Replace the current list with the new list
            _database.Meetings.FirstOrDefault().Attendees.Clear();  // Clear existing attendees
            _database.Meetings.FirstOrDefault().Attendees.AddRange(attendees);  // Add the new attendees
            Console.WriteLine("AttendeeDbService : ReplaceDbWithList");
            return Task.CompletedTask;
        }


        public Task<List<LH_AttendeeModel>> RemoveAttendeeByTemporaryId_NoJS(Guid ClientTemporaryId)
        {

            try
            {
                LH_AttendeeModel attendeeToRemove = _database.Meetings.First().Attendees.Single(x => x.ClientTemporaryId == ClientTemporaryId);

                _database.Meetings.First().Attendees.Remove(attendeeToRemove);
                //await _database.SaveChangesAsync(); isnt a real  db

                return Task.FromResult(_database.Meetings.FirstOrDefault().Attendees.ToList<LH_AttendeeModel>());

            }
            catch (Exception e)
            {
                //Not single, not first, not found - do something here
                throw;
            }

        }


    }
}
