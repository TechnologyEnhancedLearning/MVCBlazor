using LH.MVCBlazor.Server.Controllers.BaseControllers;
using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Package.LH.BlazorComponents.DependencyInjection;
using Package.LH.BlazorComponents.Models;
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


        ////!!!! This is just because of the way radiolist VC components bind name and asp-for
        //[HttpPost("SetFavouriteCharacterByForm")]//qqqq its get in example in lh
        //public async Task<IActionResult> SetFavouriteCharacterByForm(LHB_FavouriteCharacterFormModel LHB_FavouriteCharacterFormModel   /*int FavouriteCharacterId*/, string returnUrl = null)
        //{
        //    return await SetFavouriteCharacterHelper(LHB_FavouriteCharacterFormModel.FavouriteCharacterId, returnUrl);
        //}        //!!!! This is just because of the way radiolist VC components bind name and asp-for
        [HttpPost("SetFavouriteCharacterByForm")]//qqqq its get in example in lh
        public async Task<IActionResult> SetFavouriteCharacterByForm(CharactersViewModel CharactersViewModel, string returnUrl = null)
        {
            return await SetFavouriteCharacterHelper(CharactersViewModel.LHB_FavouriteCharacterFormModel.FavouriteCharacterId, returnUrl);
        }

        [HttpPost("SetFavouriteCharacter")]
        public async Task<IActionResult> SetFavouriteCharacter(int FavouriteCharacterId, string returnUrl = null)
        {
    
            return await SetFavouriteCharacterHelper(FavouriteCharacterId,  returnUrl); // Redirect back to the index after setting favorite
        }
        private async Task<IActionResult> SetFavouriteCharacterHelper(int FavouriteCharacterId, string returnUrl = null)
        {
            await _charactersStateService.SetCharacterAsFavouriteAsync(FavouriteCharacterId);
            return RedirectToReturnUrl(returnUrl); // Redirect back to the index after setting favorite
        }

        //[HttpPost("SetFavouriteCharacter")]//qqqq its get in example in lh
        //public async Task<IActionResult> SetFavouriteCharacter(GB_FavouriteCharacterFormModel GB_FavouriteCharacterFormModel, string returnUrl = null)
        //{
        //    int characterId = GB_FavouriteCharacterFormModel.FavouriteCharacterId;
        //    if (characterId <= 0)
        //    {
        //        ModelState.AddModelError("FavouriteCharacterId", "Invalid character ID provided.");
        //        //return BadRequest("Invalid character ID provided.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        //just for testing
        //        GB_FavouriteCharacterFormModel.ModelStateErrors = ModelState
        //            .Where(ms => ms.Value.Errors.Count > 0)
        //            .ToDictionary(
        //                kvp => kvp.Key,
        //                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
        //            );
        //    }


        //    await _charactersStateService.SetCharacterAsFavouriteAsync(characterId);
        //    return RedirectToReturnUrl(returnUrl); // Redirect back to the index after setting favorite
        //}


    }
}
