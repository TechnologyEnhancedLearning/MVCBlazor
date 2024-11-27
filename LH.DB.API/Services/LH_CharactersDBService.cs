using LH.DB.API.Data;
using Microsoft.AspNetCore.Mvc;
using Package.LH.Entities.Models;
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
  
        public Task<List<GE_CharacterModel>> LoadCharactersAsync()
        {
            return Task.FromResult(_characters);
        }

        public Task<List<GE_CharacterModel>> RemoveCharacterByTemporaryId_NoJS(Guid ClientTemporaryId)
        {
            try
            {
                GE_CharacterModel characterToRemove = _characters.Single(x => x.ClientTemporaryId == ClientTemporaryId);

                _characters.Remove(characterToRemove);
                //await _database.SaveChangesAsync(); isnt a real  db

                return Task.FromResult(_characters);//If real db then would really be async

            }
            catch (Exception e)
            {
                //Not single, not first, not found - do something here
                throw;
            }

        }

       

        public Task ReplaceDBWithList(List<GE_CharacterModel> characters)
        {
            // Replace the current list with the new list
            _characters.Clear();  // Clear 
            _characters.AddRange(characters);  // Add 
            return Task.CompletedTask;//If real db then would really be async
        }

        public Task SetCharacterAsFavourite(int characterId)
        {
            Console.WriteLine($"CharactersDbService : SaveFavouriteCharacterAsync for CharacterId: {characterId}");

            _characters.ForEach(x => x.IsFavourite = (x.Id == characterId));
            return Task.CompletedTask;
        }
    }
}
