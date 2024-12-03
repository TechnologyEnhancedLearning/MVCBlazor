using Microsoft.AspNetCore.Mvc;
using Package.Shared.BlazorComponents.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.BlazorComponents.Models
{
    public class LHB_FavouriteCharacterFormModel : IGB_ModelStateValidation
    {
        [Required(ErrorMessage = "A character must be selected.")]
        [Display(Name = "Favourite Character")]
        //[FromForm(Name = "LHB_FavouriteCharacterFormModel.FavouriteCharacterId")]
        public int FavouriteCharacterId { get; set; }
 



        [Required(ErrorMessage = "SUCCESS YOU'VE GOT THE ERROR FROM VIEWSTATE (if your seeing this with JSDisabled)")]
        public string TestModelStateWithRequired { get; set; } = null;
        public Dictionary<string, List<string>> ModelStateErrors { get; set; } = new();

        public bool HasModelStateValidationErrors => ModelStateErrors.Any();

        public LHB_FavouriteCharacterFormModel() { }


    }
}
