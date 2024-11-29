using LH.MVCBlazor.Server.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using Package.LH.BlazorComponents.DependencyInjection;
using Package.Shared.BlazorComponents.Models;
using Package.Shared.Services.StateServices.CharacterStateServices;

namespace LH.MVCBlazor.Server.Controllers
{
    [Route("Characters/NoJS")]
    public class Characters_NoJSController : NoJSBaseController
    {

        private readonly IGS_CharactersStateService _charactersStateService;


        protected override string DefaultRouteController { get; set; } = "Characters";
        protected override string DefaultRouteAction { get; set; } = "Index";



        public Characters_NoJSController(IGS_CharactersStateService charactersStateService, LHB_BlazorPageRegistryService blazorPageRegistryService)
            : base(blazorPageRegistryService)
        {
            _charactersStateService = charactersStateService;
        }

        [HttpPost("SetFavouriteCharacter")]//qqqq its get in example in lh
        public async Task<IActionResult> SetFavouriteCharacter([FromForm]GB_FavouriteCharacterFormModel GB_FavouriteCharacterFormModel, string returnUrl = null)
        {
            int characterId = GB_FavouriteCharacterFormModel.FavouriteCharacterId;
            if (characterId <= 0)
            {
                ModelState.AddModelError("FavouriteCharacterId", "Invalid character ID provided.");
                //return BadRequest("Invalid character ID provided.");
            }

            if (!ModelState.IsValid)
            {
                //just for testing
                GB_FavouriteCharacterFormModel.ModelStateErrors = ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    );
            }


            await _charactersStateService.SetCharacterAsFavouriteAsync(characterId);
            return RedirectToReturnUrl(returnUrl); // Redirect back to the index after setting favorite
        }
 

    }
}
