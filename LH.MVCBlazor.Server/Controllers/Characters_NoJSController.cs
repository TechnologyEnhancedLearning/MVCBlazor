using LH.MVCBlazor.Server.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using Package.LH.BlazorComponents.DependencyInjection;
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
        public async Task<IActionResult> SetFavouriteCharacter(int FavouriteCharacterId, string returnUrl = null)
        {

            if (FavouriteCharacterId <= 0)
            {
                return BadRequest("Invalid character ID provided.");
            }

            await _charactersStateService.SetCharacterAsFavouriteAsync(FavouriteCharacterId);
            return RedirectToReturnUrl(returnUrl); // Redirect back to the index after setting favorite
        }


    }
}
