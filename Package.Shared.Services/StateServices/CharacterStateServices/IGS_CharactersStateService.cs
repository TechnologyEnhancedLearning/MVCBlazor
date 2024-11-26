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
        Task<List<GE_CartoonModel>> GetCartoonsAsync();
        Task SetCharacterAsFavouriteAsync(int characterId);
    }
}
