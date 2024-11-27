using NHSUKViewComponents.Web.ViewModels;
using Package.Shared.Entities.Models;
using System.Diagnostics;

namespace LH.MVCBlazor.Server.Helpers.VCHelpers
{
    public static class CharacterRadiosHelper
    {
        public static List<RadiosItemViewModel> PrepareCharacterRadios(List<GE_CharacterModel> characters)
        {
            var radios = new List<RadiosItemViewModel>();

            foreach (var character in characters)
            {
                Debug.WriteLine(character.ToString());
                radios.Add(new RadiosItemViewModel(
                    character.Id.ToString(),
                    character.ToString(),
                    character.IsFavourite,
                    "Select this character"
                ));
            }

            return radios;
        }
    }
}
