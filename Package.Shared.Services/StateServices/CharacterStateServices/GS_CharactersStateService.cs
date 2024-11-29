using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Package.Shared.Entities;
using Package.Shared.Services.Configuration;
using Package.Shared.Entities.Models;
using Package.Shared.Entities.Communication;
using Package.Shared.Services.Configuration.CharactersConfiguration;

namespace Package.Shared.Services.StateServices.CharacterStateServices
{
    internal class GS_CharactersStateService : IGS_CharactersStateService
    {
        private readonly HttpClient _http;
        private readonly GS_CharactersAPIConfiguration _charactersAPIConfiguration; // Assuming this is defined similar to your attendee endpoints
        private readonly IGS_CharactersAPIEndpoints _charactersAPIEndpoints; // Assuming this is defined similar to your attendee endpoints
        public bool DataIsLoaded { get; private set; } = false;
        private Task _loadingTask;
        public List<GE_CharacterModel> Characters { get; private set; } = new List<GE_CharacterModel>();




        public GS_CharactersStateService(IHttpClientFactory httpClientFactory, IOptions<GS_CharactersAPIConfiguration> charactersAPIConfiguration)//qqqq i want this as an interface but wasnt previously will it error
        {
            _charactersAPIConfiguration = charactersAPIConfiguration.Value;
            _charactersAPIEndpoints = _charactersAPIConfiguration.Endpoints.Characters;
            _http = httpClientFactory.CreateClient(_charactersAPIConfiguration.ClientName);
            _loadingTask = LoadCharactersAsync();
        }

        public async Task EnsureDataIsLoadedAsync()
        {
            if (!DataIsLoaded)
            {
                await _loadingTask;
            }
        }

        public async Task<GE_ServiceResponse<List<GE_CharacterModel>>> GetCharactersAsync()
        {
            await EnsureDataIsLoadedAsync();
            return new GE_ServiceResponse<List<GE_CharacterModel>> { Data = Characters };
        }

        private async Task LoadCharactersAsync()
        {
            string route = $"{_http.BaseAddress}{_charactersAPIEndpoints.LoadCharacters}"; // Adjust this to your actual endpoint
            Characters = (await _http.GetFromJsonAsync<GE_ServiceResponse<List<GE_CharacterModel>>>(route)).Data ?? new List<GE_CharacterModel>();
            DataIsLoaded = true;
        }

        public async Task<GE_ServiceResponse<bool>> SetCharacterAsFavouriteAsync(int characterId)
        {
            string route = $"{_http.BaseAddress}{_charactersAPIEndpoints.SetCharacterAsFavourite}?characterId={characterId}";
            // Call the API to set the character as a favorite
            var response = await _http.GetAsync($"{_http.BaseAddress}{_charactersAPIEndpoints.SetCharacterAsFavourite}?characterId={characterId}");

            if (!response.IsSuccessStatusCode)
            {
                // Handle the error case
                throw new Exception("Failed to set the character as favourite.");
            }

            return new GE_ServiceResponse<bool> { Data = true };
        }

        public async Task<GE_ServiceResponse<bool>> AddCharacterAsync(GE_CharacterModel character)
        {
            await EnsureDataIsLoadedAsync();
            if (character != null)
            {
                Characters.Add(character);
            }
            Console.WriteLine("CharactersStateService: AddCharacter");

            return new GE_ServiceResponse<bool> { Data = true };
        }

        public async Task<GE_ServiceResponse<bool>> RemoveCharacterByTemporaryIdAsync(Guid clientTemporaryId)
        {
            await EnsureDataIsLoadedAsync();
            var attendee = Characters.Find(a => a.ClientTemporaryId == clientTemporaryId);
            if (attendee != null)
            {
                Characters.Remove(attendee);
            }
            Console.WriteLine("CharactersStateService : Removed");

            return new GE_ServiceResponse<bool> { Data = true };
        }

        public async Task<GE_ServiceResponse<List<GE_CharacterModel>>> ReplaceDBWithListAsync()
        {
            await EnsureDataIsLoadedAsync();


            // Send the current list of attendees to the server to replace the existing database records
            var response = await _http.PostAsJsonAsync($"{_http.BaseAddress}{_charactersAPIEndpoints.ReplaceDBWithList}", Characters);

            if (response.IsSuccessStatusCode)
            {

                var updatedAttendees = await response.Content.ReadFromJsonAsync<List<GE_CharacterModel>>();

                Characters = updatedAttendees ?? new List<GE_CharacterModel>();
            }
            else
            {
              
                throw new Exception("Failed to replace the database and retrieve updated characters.");
            }

            // Return the updated list of attendees
            Console.WriteLine("CharactersStateService: ReplaceDBWithList");

            return new GE_ServiceResponse<List<GE_CharacterModel>>{ Data = Characters };
        }
    }
}
