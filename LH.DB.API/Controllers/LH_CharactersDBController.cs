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
        public async Task<ActionResult<GE_ServiceResponse<List<GE_CharacterModel>>>> LoadCharacters()
        {
            var result = await _charactersDbService.LoadCharactersAsync();

            // Return a standardized ServiceResponse
            return Ok(result);
        }

        // POST: api/characters/replaceDBWithList
        [HttpPost("replaceDBWithList")]
        public async Task<ActionResult<GE_ServiceResponse<List<GE_CharacterModel>>>> ReplaceDBWithList([FromBody] List<GE_CharacterModel> characters)
        {
            

            // Update the database with the provided characters list
            await _charactersDbService.ReplaceDBWithList(characters);

            // Load the updated characters
            var result = await _charactersDbService.LoadCharactersAsync();

            // Return the updated characters in a standardized ServiceResponse
           return Ok(result);
        }

        [HttpGet("SetCharacterAsFavourite")]
        public async Task<ActionResult<GE_ServiceResponse<List<GE_CharacterModel>>>> SetCharacterAsFavourite(int characterId)
        {

            // Call your service to save the favorite character logic
            await _charactersDbService.SetCharacterAsFavourite(characterId);

            // Load the updated characters
            var result = await _charactersDbService.LoadCharactersAsync();

            Console.WriteLine($"API : Favorite character saved for Character ID: {characterId}");
            return Ok(result);
        }
    }
}
