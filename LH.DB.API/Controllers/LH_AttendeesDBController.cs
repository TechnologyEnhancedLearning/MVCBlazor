using LH.DB.API.Services;
using Microsoft.AspNetCore.Mvc;
using Package.LH.Entities.Models;
using Package.LH.Services.Interfaces;
using Package.Shared.Entities.Communication;

namespace LH.DB.API.Controllers
{
    [ApiController]
    [Route("api/lh-db/attendees")]
    public class LH_AttendeesDBController : ControllerBase
    {
        private readonly ILHS_AttendeesDbService _attendeesDbService;

        public LH_AttendeesDBController(ILHS_AttendeesDbService attendeesDbService)
        {
            _attendeesDbService = attendeesDbService;
            Console.WriteLine("API : Constructor");
        }

   
        [HttpGet("loadAttendees")]
        public async Task<ActionResult<GE_ServiceResponse<List<LH_AttendeeModel>>>> LoadAttendees()
        {
            var result = await _attendeesDbService.LoadAttendeesAsync();

            return Ok(result); 
        }

 
        [HttpPost("replaceDBWithList")]
        public async Task<ActionResult<GE_ServiceResponse<List<LH_AttendeeModel>>>> ReplaceDBWithList([FromBody] List<LH_AttendeeModel> attendees)
        {
            if (attendees == null)
            {
                // Return a standardized error response
                return new GE_ServiceResponse<List<LH_AttendeeModel>>
                {
                    Data = null,
                    Success = false,
                    Message = "Attendees list is null."
                };
            }

            // Update the database with the provided attendees list
            await _attendeesDbService.ReplaceDBWithList(attendees);

            // Load the updated attendees
            var result = await _attendeesDbService.LoadAttendeesAsync();


            return Ok(result);
        }
    }
}
