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
using Package.Shared.Services.Configurations;
using Package.Shared.Entities.Models;

namespace Package.Shared.Services.StateServices.CharacterStateServices
{
    internal class GS_CharactersStateService : IGS_CharactersStateService
    {
        private readonly HttpClient _http;
        private readonly IGS_CharactersAPIEndPoints _charactersAPIEndPoints; // Assuming this is defined similar to your attendee endpoints
        public bool DataIsLoaded { get; private set; } = false;
        private Task _loadingTask;
        public List<GE_CartoonModel> Cartoons { get; private set; } = new List<GE_CartoonModel>();


        //So i kind of want to IGS_CharactersAPIEndPoints needs to be LHServerClientEndPointConfiguration
        public GS_CharactersStateService(IHttpClientFactory httpClientFactory, IOptions<GS_CharactersAPIEndPoints> charactersAPIEndPoints)//qqqq i want this as an interface but wasnt previously will it error
        {
            _charactersAPIEndPoints = charactersAPIEndPoints.Value;
            _http = httpClientFactory.CreateClient(charactersAPIEndPoints.Value.ClientName);
            _loadingTask = LoadCartoonsAsync();
        }

        public async Task EnsureDataIsLoadedAsync()
        {
            if (!DataIsLoaded)
            {
                await _loadingTask;
            }
        }

        public async Task<List<GE_Characters>> GetCartoonsAsync()
        {
            await EnsureDataIsLoadedAsync();
            return Cartoons;
        }

        private async Task LoadCharactersAsync()
        {
            string route = $"{_http.BaseAddress}{_charactersAPIEndPoints.LoadCartoons}"; // Adjust this to your actual endpoint
            Cartoons = await _http.GetFromJsonAsync<List<GE_CartoonModel>>(route) ?? new List<GE_CartoonModel>();
            DataIsLoaded = true;
        }

        public async Task SetCharacterAsFavouriteAsync(int characterId)
        {
            string route = $"{_http.BaseAddress}{_charactersAPIEndPoints.SetCharacterAsFavourite}?characterId={characterId}";
            // Call the API to set the character as a favorite
            var response = await _http.GetAsync($"{_http.BaseAddress}{_charactersAPIEndPoints.SetCharacterAsFavourite}?characterId={characterId}");

            if (!response.IsSuccessStatusCode)
            {
                // Handle the error case
                throw new Exception("Failed to set the character as favourite.");
            }
        }
    }
}
