using Microsoft.AspNetCore.Mvc;
using Package.LH.BlazorComponents.Models;
using Package.LH.Entities.Models;


using Package.Shared.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace LH.MVCBlazor.Server.ViewModels
{
    public class CharactersViewModel
    {
    

        public GE_FavouriteCharacterFormModel LHB_FavouriteCharacterFormModel { get; set; } = new();
        public string some_CharactersViewModel_specific_UI_String { get; set; } = "UnSet";
        public List<GE_CharacterModel> Characters { get; set; } = null;

        public string FavouriteCharacterIdStr
        {
            get => LHB_FavouriteCharacterFormModel.FavouriteCharacterId.ToString();
            set => LHB_FavouriteCharacterFormModel.FavouriteCharacterId = Int32.Parse(value);
        }


        // QQQQ Logic here to handle model state for noJS should it be
        public CharactersViewModel(List<GE_CharacterModel> characters, GE_FavouriteCharacterFormModel CurrentFormData = null)
        {
            LHB_FavouriteCharacterFormModel = CurrentFormData?? LHB_FavouriteCharacterFormModel;
            
            if (CurrentFormData != null && CurrentFormData.FavouriteCharacterId != 0)
            {
                //set favourite from form
                characters.ForEach(x => x.IsFavourite = false);
                characters.Single(x => x.Id == CurrentFormData.FavouriteCharacterId).IsFavourite = true;
                Characters = characters;
                
            }
            else 
            {
                //Set favourite from database
                Characters = characters;
                LHB_FavouriteCharacterFormModel.FavouriteCharacterId = characters.Single(x => x.IsFavourite).Id;

            }
            FavouriteCharacterIdStr = LHB_FavouriteCharacterFormModel.FavouriteCharacterId.ToString();
        }

        public CharactersViewModel()
        {

        }
    }
}
