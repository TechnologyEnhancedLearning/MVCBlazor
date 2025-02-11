using LH.DB.API.Data;
using Microsoft.AspNetCore.Mvc;
using Package.Shared.Entities.Communication;
using Serilog;
using System.Text.Json;

namespace LH.DB.API.Services
{
    public class T_LoggerDBService
    {
        private readonly ISimulatedDatabase _database;
        //private readonly List<GE_CharacterModel> _characters;

        public T_LoggerDBService(ISimulatedDatabase database)
        {
            _database = database;
            //For the current scope just want a list

        }

        public async Task<GE_ServiceResponse<List<string>>> InsertLogsToDB(string logs)//qqqq we will define a standard log object, probably the existing structure
        {
            await Task.Delay(100);//some db work
            _database.Logs.Add(logs);
            return new GE_ServiceResponse<List<string>> { Data = _database.Logs };
        }


        public async Task<GE_ServiceResponse<List<string>>> GetLogsFromDB()
        {
            await Task.Delay(100);//some db work
            return new GE_ServiceResponse<List<string>> { Data = _database.Logs };

        }
}
    }
