using LH.MVCBlazor.Server.Controllers.BaseControllers;
using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Package.LH.BlazorComponents.DependencyInjection;
using Package.LH.BlazorComponents.Models;
using Package.Shared.BlazorComponents.Enums;
using Package.Shared.Services.StateServices.CharacterStateServices;
using System.Reflection;
using static LH.MVCBlazor.Server.Helpers.ControllerHelpers.ControllerHelper;

namespace LH.MVCBlazor.Server.Controllers
{
    [Route("Characters/NoJS")]
    public class Characters_NoJSController : NoJSBaseController
    {

        private readonly IGS_CharactersStateService _charactersStateService;

        protected override string DefaultViewRouteController { get; set; } = "~/Views/Characters/Index.cshtml";
        protected override string DefaultRouteController { get; set; } = "Characters";
        protected override string DefaultRouteAction { get; set; } = "Index";
        protected override string RedirectBlazorPagesNoJsStaticMVCRoute { get; set; } = "/Characters/Static-MVCRendered";

        public Characters_NoJSController(IGS_CharactersStateService charactersStateService, LHB_BlazorPageRegistryService blazorPageRegistryService)
            : base(blazorPageRegistryService)
        {
            _charactersStateService = charactersStateService;
        }


        ////!!!! This is just because of the way radiolist VC components bind name and asp-for
        [HttpPost("SetFavouriteCharacterByTopLevelForm")]
        public async Task<IActionResult> SetFavouriteCharacterByTopLevelForm(CharactersViewModel CharactersViewModel, string returnUrl = null)
        {
            return await SetFavouriteCharacterHelper(CharactersViewModel.LHB_FavouriteCharacterFormModel.FavouriteCharacterId, returnUrl);
        }
        [HttpPost("SetFavouriteCharacterByForm")]
        public async Task<IActionResult> SetFavouriteCharacterByForm(GE_FavouriteCharacterFormModel LHB_FavouriteCharacterFormModel, string returnUrl = null)
        {


            if (!ModelState.IsValid)
            {

                LHB_FavouriteCharacterFormModel.ModelStateErrors = GetModelStateDictionary(ModelState);

                var viewModel = new CharactersViewModel((await _charactersStateService.GetCharactersAsync()).Data, LHB_FavouriteCharacterFormModel);
          
                ViewBag.RenderMode = GetRenderModeStr(HttpContext.Request.Path.Value,"Characters");

           
                if (IsBlazorPage())
                {
                    //cant pass the validation data with current set up so redirect to the static page
                    TempData["CharactersData"] = JsonConvert.SerializeObject(viewModel); 
                    return RedirectToAction("Static-MVCRendered", "Characters");
                  

                }
                else
                {
                    //its mvc so just return model to where it came from now it has validation state errors on it
                    TempData["CharactersData"] = JsonConvert.SerializeObject(viewModel);

                    returnUrl = returnUrl ?? Request.Headers["Referer"].ToString();
                    //"https://localhost:44343/Characters/ServerPrerendered-MVCRendered"
                    return RedirectToAction(returnUrl.Split('/').Last(), "Characters");

      
                }
                
            }


            return await SetFavouriteCharacterHelper(LHB_FavouriteCharacterFormModel.FavouriteCharacterId, returnUrl);
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

       

     


    }
}
