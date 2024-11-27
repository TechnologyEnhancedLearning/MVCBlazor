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
        Task EnsureDataIsLoadedAsync();
        Task<List<GE_CharacterModel>> GetCharactersAsync();
        Task SetCharacterAsFavouriteAsync(int characterId);
        Task AddCharacterAsync(GE_CharacterModel character);

        Task RemoveCharacterByTemporaryIdAsync(Guid clientTemporaryId);

        Task<List<GE_CharacterModel>> ReplaceDBWithListAsync();

        bool DataIsLoaded { get; }

        List<GE_CharacterModel> Characters { get; }
    }
}
