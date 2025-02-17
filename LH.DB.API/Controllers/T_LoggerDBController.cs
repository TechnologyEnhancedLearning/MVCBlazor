using LH.DB.API.Services;
using Microsoft.AspNetCore.Mvc;
using Package.Shared.Entities.Communication;
using Package.Shared.Services.Interfaces;
using System.Text.Json;

namespace LH.DB.API.Controllers
{
    [ApiController]
    [Route("api/lh-db/logs")]
    public class T_LoggerDBController : ControllerBase
    {
        private readonly T_LoggerDBService _loggerDBService;
        public T_LoggerDBController(T_LoggerDBService loggerDBService)
        {
            _loggerDBService = loggerDBService;
        }

        /// <summary>
        /// Note: We would define a structured log object
        /// </summary>
        /// <param name="structuredLogs"></param>
        /// <returns></returns>
        [HttpPost("InsertLogs")]
        public async Task<ActionResult<GE_ServiceResponse<List<string>>>> InsertLogs([FromBody] JsonElement structuredLogs)
        {

            var result = await _loggerDBService.InsertLogsToDB(structuredLogs.GetRawText());


            // Return the updated characters in a standardized ServiceResponse
            return Ok(result);

        }
        [HttpGet("GetLogs")]
        public async Task<ActionResult<GE_ServiceResponse<List<string>>>> GetLogs()
        {

            var result = await _loggerDBService.GetLogsFromDB();

            // Return a standardized ServiceResponse
            return Ok(result);


        }
    }
}


