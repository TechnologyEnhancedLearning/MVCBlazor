using LH.DB.API.Data;
using Microsoft.AspNetCore.Mvc;
using Package.LH.Entities.Models;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using Package.Shared.Services.Interfaces;

namespace LH.DB.API.Services
{
    public class T_CounterDBService : IT_CounterDBService
    {
        private readonly ISimulatedDatabase _database;
        //private readonly List<GE_CharacterModel> _characters;

        public T_CounterDBService(ISimulatedDatabase database)
        {
            _database = database;
            //For the current scope just want a list

        }
        [HttpGet("GetCountFromDB")]
        public async Task<GE_ServiceResponse<string>> GetCountFromDB()
        {
            return new GE_ServiceResponse<string> { Data = _database.ClickCount };
        }
        [HttpGet("SetCountInDB")]
        public async Task<GE_ServiceResponse<string>> SetCountInDB(string count)
        {
            _database.ClickCount = count;
            return new GE_ServiceResponse<string> { Data = _database.ClickCount };
        }

    }
}
