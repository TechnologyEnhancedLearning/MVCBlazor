using Package.LH.Entities.Models;
using Package.LH.Services.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.StateServices
{
    public class LHS_AttendeesStateService: ILHS_AttendeesStateService //qqqq maybe should be in server side qqqq
    {
        private readonly HttpClient _http;
        private ILHS_AttendeesAPIEndPoints _attendeesAPIEndPoints;
        public bool DataIsLoaded { get; private set; } = false;
        private Task _loadingTask;
        public List<LH_AttendeeModel> Attendees { get; private set; } = new List<LH_AttendeeModel>();

        public AttendeesStateService(IHttpClientFactory httpClientFactory, IOptions<LHS_AttendeesAPIEndPoints> attendeesAPIEndPoints)
        {
            // _http = http;
            _attendeesAPIEndPoints = attendeesAPIEndPoints.Value;
            _http = httpClientFactory.CreateClient(attendeesAPIEndPoints.Value.ClientName);
            _loadingTask = LoadAttendeesAsync(); //cant await in constuctor and often bad to load like this but we want to kick off the load from the beginning and just wait for it to finish when data requested
        }


        public async Task EnsureDataIsLoadedAsync() //By always checking preload we can let the constructor preload unawaited
        {
            if (!DataIsLoaded)
            {
                // Await the loading task if data is still being loaded
                await _loadingTask;
            }
        }
        public async Task<List<LH_AttendeeModel>> GetAttendeesAsync()
        {
            await EnsureDataIsLoadedAsync();
            return Attendees;
        }
        private async Task LoadAttendeesAsync()
        {


            // Fetch attendees from the server
            string route = $"{_http.BaseAddress}{_attendeesAPIEndPoints.LoadAttendees}";
            Console.WriteLine(route);

            Attendees = await _http.GetFromJsonAsync<List<LH_AttendeeModel>>($"{_http.BaseAddress}{_attendeesAPIEndPoints.LoadAttendees}") ?? new List<LH_AttendeeModel>();
            DataIsLoaded = true; // Set the flag to true when data is loaded
            Console.WriteLine("AttendeeStateService: LoadAttendeesAsync");
        }

        public async Task AddAttendeeAsync(LH_AttendeeModel attendee)
        {
            await EnsureDataIsLoadedAsync();
            if (attendee != null)
            {
                Attendees.Add(attendee);
            }
            Console.WriteLine("AttendeesStateService: AddAttendee");
        }

        public async Task RemoveAttendeeByTemporaryIdAsync(Guid clientTemporaryId)
        {
            await EnsureDataIsLoadedAsync();
            var attendee = Attendees.Find(a => a.ClientTemporaryId == clientTemporaryId);
            if (attendee != null)
            {
                Attendees.Remove(attendee);
            }
            Console.WriteLine("AttendeesStateService : Removed");
        }

        public async Task<List<LH_AttendeeModel>> ReplaceDbWithListAsync()
        {
            await EnsureDataIsLoadedAsync();


            // Send the current list of attendees to the server to replace the existing database records
            var response = await _http.PostAsJsonAsync($"{_http.BaseAddress}{_attendeeAPIEndPoints.ReplaceDbWithList}", Attendees);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as the updated list of attendees
                var updatedAttendees = await response.Content.ReadFromJsonAsync<List<LH_AttendeeModel>>();

                // Update the local list of attendees
                Attendees = updatedAttendees ?? new List<LH_AttendeeModel>();
            }
            else
            {
                // Handle failure case (throw an exception or return an empty list)
                throw new Exception("Failed to replace the database and retrieve updated attendees.");
            }

            // Return the updated list of attendees
            Console.WriteLine("AttendeeStateService: ReplaceDbWithList");
            return Attendees;
        }

     
    }
}
