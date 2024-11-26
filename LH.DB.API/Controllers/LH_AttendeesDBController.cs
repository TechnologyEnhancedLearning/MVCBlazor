﻿using Microsoft.AspNetCore.Mvc;
using Package.Shared.Entities.Communication;

namespace LH.DB.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LH_AttendeesDBController : ControllerBase
    {
        private readonly IAttendeesDbService _attendeesDbService;

        public LH_AttendeesDBController(IAttendeesDbService attendeesDbService)
        {
            _attendeesDbService = attendeesDbService;
            Console.WriteLine("API : Constructor");
        }

        // GET: api/attendees/loadAttendees
        [HttpGet("loadAttendees")]
        public async Task<GE_ServiceResponse<List<LH_AttendeeModel>>> LoadAttendees()
        {
            var attendees = await _attendeesDbService.LoadAttendeesAsync();

            // Return a standardized ServiceResponse
            return new GE_ServiceResponse<List<LH_AttendeeModel>>
            {
                Data = attendees,
                Success = true,
                Message = "Attendees loaded successfully."
            };
        }

        // POST: api/attendees/replaceDbWithList
        [HttpPost("replaceDbWithList")]
        public async Task<GE_ServiceResponse<List<LH_AttendeeModel>>> ReplaceDbWithList([FromBody] List<LH_AttendeeModel> attendees)
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
            await _attendeesDbService.ReplaceDbWithList(attendees);

            // Load the updated attendees
            var updatedAttendees = await _attendeesDbService.LoadAttendeesAsync();

            // Return the updated attendees in a standardized ServiceResponse
            return new GE_ServiceResponse<List<LH_AttendeeModel>>
            {
                Data = updatedAttendees,
                Success = true,
                Message = "Database replaced and updated attendees retrieved successfully."
            };
        }
    }
}
