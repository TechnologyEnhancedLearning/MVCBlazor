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
        /// <summary>
        /// Asynchronously loads all cartoons along with their characters.
        /// </summary>
        /// <returns>A list of GE_CartoonModel containing cartoons and their respective characters.</returns>
        Task<List<GE_CartoonModel>> LoadCartoonsAsync();

        /// <summary>
        /// Asynchronously saves the selected favorite character for a specific cartoon.
        /// </summary>
        /// <param name="cartoonId">The ID of the cartoon.</param>
        /// <param name="characterId">The ID of the character that is selected as favorite.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        Task SetCharacterAsFavouriteForTheirCartoon(int characterId);

    }
}
