using LH.MVCBlazor.Server.Controllers.BaseControllers;
using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Package.LH.BlazorComponents.DependencyInjection;
using Package.LH.BlazorComponents.Models;
using Package.Shared.BlazorComponents.Enums;
using Package.Shared.Services.StateServices.CharacterStateServices;
using System.Reflection;

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
        //[HttpPost("SetFavouriteCharacterByTopLevelForm")]//qqqq its get in example in lh
        //public async Task<IActionResult> SetFavouriteCharacterByTopLevelForm(LHB_FavouriteCharacterFormModel LHB_FavouriteCharacterFormModel   /*int FavouriteCharacterId*/, string returnUrl = null)
        //{
        //    return await SetFavouriteCharacterHelper(LHB_FavouriteCharacterFormModel.FavouriteCharacterId, returnUrl);
        //}        //!!!! This is just because of the way radiolist VC components bind name and asp-for
        [HttpPost("SetFavouriteCharacterByTopLevelForm")]//qqqq its get in example in lh
        public async Task<IActionResult> SetFavouriteCharacterByTopLevelForm(CharactersViewModel CharactersViewModel, string returnUrl = null)
        {
            CharactersViewModel.LHB_FavouriteCharacterFormModel.ModelStateErrors = GetModelStateDictionary(ModelState);
            //qqqq then we would need to redirect to returning the model with the view
            return await SetFavouriteCharacterHelper(CharactersViewModel.LHB_FavouriteCharacterFormModel.FavouriteCharacterId, returnUrl);
        }
        [HttpPost("SetFavouriteCharacterByForm")]//qqqq its get in example in lh
        public async Task<IActionResult> SetFavouriteCharacterByForm(GE_FavouriteCharacterFormModel LHB_FavouriteCharacterFormModel, string returnUrl = null)
        {


            if (!ModelState.IsValid)
            {

                LHB_FavouriteCharacterFormModel.ModelStateErrors = GetModelStateDictionary(ModelState);

                var viewModel = new CharactersViewModel((await _charactersStateService.GetCharactersAsync()).Data, LHB_FavouriteCharacterFormModel);
          
                ViewBag.RenderMode = GetRenderModeStr();

                bool IsBlazorPage = true;
                if (IsBlazorPage)
                {
                    //cant pass the validation data with current set up so redirect to the static page
                    TempData["CharactersData"] = JsonConvert.SerializeObject(viewModel); // Serialize to pass complex objects
                    return RedirectToAction("Static-MVCRendered", "Characters");
                    //return RedirectToAction("Index", "Characters/Static-MVCRendered");

                }
                else
                {
                    //its mvc so just return model to where it came from now it has validation state errors on it

                    return ReturnViewWithModel(viewModel);
                }
                
            }

            //qqqq then we would need to redirect to returning the model with the view
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

        private string GetRenderModeStr()
        {
            // Get the current route from the request path
            string route = HttpContext.Request.Path.Value; // E.g., "/Attendees/Static-MVCRendered"

            // Extract the part of the route that matches the render mode
            string renderModeString = route.Replace("/Characters/", "").Replace("-MVCRendered", "");

            // Try to parse the extracted string into the enum
            if (Enum.TryParse(renderModeString, true, out GB_ComponentTagRenderMode renderMode))
            {
                // Successfully parsed render mode
            }
            else
            {
                // Default to Static if no match
                renderMode = GB_ComponentTagRenderMode.Static;
                //This could be the noJs so lets do static as a fallback
                //throw new Exception("Rendermode not in the enum"); //this is just for convenience we wouldnt have render mode routes
            }

            return renderMode.ToString();
        }

     


    }
}
