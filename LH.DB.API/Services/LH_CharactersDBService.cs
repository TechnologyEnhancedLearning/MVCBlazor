using LH.DB.API.Data;
using Microsoft.AspNetCore.Mvc;
using Package.LH.Entities.Models;
using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using Package.Shared.Services.Interfaces;

namespace LH.DB.API.Services
{
    public class LH_CharactersDBService : IGS_CharactersDBService
    {
        private readonly ISimulatedDatabase _database;
        private readonly List<GE_CharacterModel> _characters;

        public LH_CharactersDBService(ISimulatedDatabase database)
        {
            _database = database;
            //For the current scope just want a list
            _characters = _database.Cartoons.First().People;
        }
  
        public async Task<GE_ServiceResponse<List<GE_CharacterModel>>> LoadCharactersAsync()
        {
            return new GE_ServiceResponse<List<GE_CharacterModel>>{ Data = _characters };
        }

        public async Task<GE_ServiceResponse<bool>> RemoveCharacterByTemporaryId_NoJS(Guid ClientTemporaryId)
        {
            try
            {
                GE_CharacterModel characterToRemove = _characters.Single(x => x.ClientTemporaryId == ClientTemporaryId);

                _characters.Remove(characterToRemove);
                //await _database.SaveChangesAsync(); isnt a real  db

                return new GE_ServiceResponse<bool>(){ Data = true };//If real db then would really be async

            }
            catch (Exception e)
            {
                //Not single, not first, not found - do something here
                throw;
            }

        }

       

        public async Task<GE_ServiceResponse<List<GE_CharacterModel>>> ReplaceDBWithList(List<GE_CharacterModel> characters)
        {
            // Replace the current list with the new list
            _characters.Clear();  // Clear 
            _characters.AddRange(characters);  // Add 
            return new GE_ServiceResponse<List<GE_CharacterModel>> {Data=_characters };//If real db then would really be async
        }

        public async Task<GE_ServiceResponse<bool>> SetCharacterAsFavourite(int characterId)
        {
            Console.WriteLine($"CharactersDbService : SaveFavouriteCharacterAsync for CharacterId: {characterId}");

            _characters.ForEach(x => x.IsFavourite = (x.Id == characterId));
            return new GE_ServiceResponse<bool> { Data = true };
        }
    }
}
