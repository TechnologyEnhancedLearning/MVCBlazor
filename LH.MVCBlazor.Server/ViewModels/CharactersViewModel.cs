using Package.LH.Entities.Models;
using Package.Shared.BlazorComponents.Interfaces;
using Package.Shared.BlazorComponents.Models;
using Package.Shared.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace LH.MVCBlazor.Server.ViewModels
{
    public class CharactersViewModel
    {
        //Kind of makes sense to be list cartoon qqqq

        public GB_FavouriteCharacterFormModel GB_FavouriteCharacterFormModel { get; set; } = new GB_FavouriteCharacterFormModel();
        public string some_CharactersViewModel_specific_UI_String { get; set; } = "UnSet";
        public List<GE_CharacterModel> Characters { get; set; }
        

        public CharactersViewModel(List<GE_CharacterModel> characters)
        {
            Characters = characters;
            GB_FavouriteCharacterFormModel.FavouriteCharacterId = characters.Single(x => x.IsFavourite).Id;
        }
    }
}
