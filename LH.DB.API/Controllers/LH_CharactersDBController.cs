using LH.DB.API.Services;
using Microsoft.AspNetCore.Mvc;
using Package.LH.Entities.Models;
using Package.LH.Services.Interfaces;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using Package.Shared.Services.Interfaces;

namespace LH.DB.API.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/lh-db/characters")]
    public class LH_CharactersDBController : ControllerBase
    {
        private readonly IGS_CharactersDBService _charactersDbService;

        public LH_CharactersDBController(IGS_CharactersDBService charactersDbService)
        {
            _charactersDbService = charactersDbService;
            Console.WriteLine("API : Constructor");
        }

        // GET: api/characters/loadCharacters
        [HttpGet("loadCharacters")]
        public async Task<GE_ServiceResponse<List<GE_CharacterModel>>> LoadCharacters()
        {
            var characters = await _charactersDbService.LoadCharactersAsync();

            // Return a standardized ServiceResponse
            return new GE_ServiceResponse<List<GE_CharacterModel>>
            {
                Data = characters,
                Success = true,
                Message = "Characters loaded successfully."
            };
        }

        // POST: api/characters/replaceDBWithList
        [HttpPost("replaceDBWithList")]
        public async Task<GE_ServiceResponse<List<GE_CharacterModel>>> ReplaceDBWithList([FromBody] List<GE_CharacterModel> characters)
        {
            if (characters == null)
            {
                // Return a standardized error response
                return new GE_ServiceResponse<List<GE_CharacterModel>>
                {
                    Data = null,
                    Success = false,
                    Message = "Characters list is null."
                };
            }

            // Update the database with the provided characters list
            await _charactersDbService.ReplaceDBWithList(characters);

            // Load the updated characters
            var updatedCharacters = await _charactersDbService.LoadCharactersAsync();

            // Return the updated characters in a standardized ServiceResponse
            return new GE_ServiceResponse<List<GE_CharacterModel>>
            {
                Data = updatedCharacters,
                Success = true,
                Message = "Database replaced and updated characters retrieved successfully."
            };
        }

        [HttpGet("SetCharacterAsFavourite")]
        public async Task<GE_ServiceResponse<List<GE_CharacterModel>>> SetCharacterAsFavourite(int characterId)
        {

            // Call your service to save the favorite character logic
            await _charactersDbService.SetCharacterAsFavourite(characterId);

            // Load the updated characters
            var updatedCharacters = await _charactersDbService.LoadCharactersAsync();

            Console.WriteLine($"API : Favorite character saved for Character ID: {characterId}");
            return new GE_ServiceResponse<List<GE_CharacterModel>>
            {
                Data = updatedCharacters,
                Success = true,
                Message = "Database replaced and updated characters retrieved successfully."
            };
        }
    }
}
