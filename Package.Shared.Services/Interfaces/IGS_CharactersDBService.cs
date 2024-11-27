using Package.Shared.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Interfaces
{
    public interface IGS_CharactersDBService
    {
   
        Task<List<GE_CharacterModel>> LoadCharactersAsync();

        Task SetCharacterAsFavourite(int characterId);

        public Task ReplaceDBWithList(List<GE_CharacterModel> characters);
        public Task<List<GE_CharacterModel>> RemoveCharacterByTemporaryId_NoJS(Guid ClientTemporaryId);

    }
}
