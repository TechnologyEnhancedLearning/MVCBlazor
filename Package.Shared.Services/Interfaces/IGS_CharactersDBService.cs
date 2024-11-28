using Package.Shared.Entities.Communication;
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

        public Task<GE_ServiceResponse<List<GE_CharacterModel>>> LoadCharactersAsync();

        public Task<GE_ServiceResponse<bool>> SetCharacterAsFavourite(int characterId);

        public Task<GE_ServiceResponse<List<GE_CharacterModel>>> ReplaceDBWithList(List<GE_CharacterModel> characters);
        public Task<GE_ServiceResponse<bool>> RemoveCharacterByTemporaryId_NoJS(Guid ClientTemporaryId);

    }
}
