using Microsoft.AspNetCore.Mvc;
using Package.LH.BlazorComponents.Models;
using Package.LH.Entities.Models;


using Package.Shared.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace LH.MVCBlazor.Server.ViewModels
{
    public class CharactersViewModel
    {
        //Kind of makes sense to be list cartoon qqqq

        public GE_FavouriteCharacterFormModel LHB_FavouriteCharacterFormModel { get; set; } = new();//qqqqqq new LHB_FavouriteCharacterFormModel();
        public string some_CharactersViewModel_specific_UI_String { get; set; } = "UnSet";
        public List<GE_CharacterModel> Characters { get; set; } = null;//qqqq new List<GE_CharacterModel>();

        //asp-for doesnt allow seperate name setting so model and controller must match
        //radiolist not detecting LHB_FavouriteCharacterFormModel.FavouriteCharacterIdStr and doesnt set it on initial load unless its string
        public string FavouriteCharacterIdStr
        {
            get => LHB_FavouriteCharacterFormModel.FavouriteCharacterId.ToString();
            set => LHB_FavouriteCharacterFormModel.FavouriteCharacterId = Int32.Parse(value);
        }



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
