using Package.Shared.Entities.Communication;
using Package.Shared.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.StateServices.CharacterStateServices
{
    public interface IGS_CharactersStateService
    {
        public string TestString { get; set; }
        Task EnsureDataIsLoadedAsync();
        Task<GE_ServiceResponse<List<GE_CharacterModel>>> GetCharactersAsync();
        Task<GE_ServiceResponse<bool>> SetCharacterAsFavouriteAsync(int characterId);
        Task<GE_ServiceResponse<bool>> AddCharacterAsync(GE_CharacterModel character);

        Task<GE_ServiceResponse<bool>> RemoveCharacterByTemporaryIdAsync(Guid clientTemporaryId);

        Task<GE_ServiceResponse<List<GE_CharacterModel>>> ReplaceDBWithListAsync();

        bool DataIsLoaded { get; }

        List<GE_CharacterModel> Characters { get; }
    }
}
