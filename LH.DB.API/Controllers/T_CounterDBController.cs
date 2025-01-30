using Microsoft.AspNetCore.Mvc;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using Package.Shared.Services.Interfaces;

namespace LH.DB.API.Controllers
{


    [ApiController]
    [Route("api/lh-db/counter")]
    public class T_CounterDBController : ControllerBase
    {
        private readonly IT_CounterDBService _counterDBService;

        public T_CounterDBController(IT_CounterDBService counterDBService)
        {
            _counterDBService = counterDBService;

        }

        [HttpGet("loadCharacters")]
        public async Task<ActionResult<GE_ServiceResponse<List<GE_CharacterModel>>>> LoadCharacters()
        {
            var result = await _charactersDbService.LoadCharactersAsync();

            // Return a standardized ServiceResponse
            return Ok(result);
        }

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

    }
}
