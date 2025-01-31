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

        [HttpGet("GetCountFromDB")]
        public async  Task<ActionResult<GE_ServiceResponse<string>>> GetCountFromDB()
        {
            var result = await _counterDBService.GetCountFromDB();

            // Return a standardized ServiceResponse
             return Ok(result);

        }

        [HttpPost("SetCountInDB")]
        public async Task<ActionResult<GE_ServiceResponse<string>>> SetCountInDB([FromBody]string count)
        {

            // Update the database with the provided characters list
            await _counterDBService.SetCountInDB(count);

            // Load the updated characters
            var result = await _counterDBService.GetCountFromDB();

            // Return the updated characters in a standardized ServiceResponse
            return Ok(result);
         
        }

    }
}
