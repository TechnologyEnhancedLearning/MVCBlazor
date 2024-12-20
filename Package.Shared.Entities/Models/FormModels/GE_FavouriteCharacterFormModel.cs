
using Package.Shared.Entities.Interfaces.ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.BlazorComponents.Models
{
    public class GE_FavouriteCharacterFormModel : IGE_ModelStateValidation
    {
        [Required(ErrorMessage = "A character must be selected.")]
        [Display(Name = "Favourite Character")]
        //[FromForm(Name = "LHB_FavouriteCharacterFormModel.FavouriteCharacterId")]
        public int FavouriteCharacterId { get; set; }

        [Required(ErrorMessage = "Required attribue error for text box")]
        public string TestModelStateWithRequired { get; set; } = null;
        public Dictionary<string, List<string>> ModelStateErrors { get; set; } = new();

        public bool HasModelStateValidationErrors => ModelStateErrors.Any();

        public GE_FavouriteCharacterFormModel() { }


    }
}
