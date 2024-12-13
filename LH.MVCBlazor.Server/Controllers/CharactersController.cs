using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Package.Shared.BlazorComponents.Enums;
using Package.Shared.Services.StateServices.CharacterStateServices;
using static LH.MVCBlazor.Server.Helpers.ControllerHelpers.ControllerHelper;

namespace LH.MVCBlazor.Server.Controllers
{
    [Route("Characters")]
    public class CharactersController : Controller
    {
        private readonly IGS_CharactersStateService _charactersStateService;


        public CharactersController(IGS_CharactersStateService charactersStateService)
        {
            _charactersStateService = charactersStateService;
        }

        //Here the MVCRendered is added because we can render these same modes in our Blazor pages and we dont have to have seperate path stems for our blazor pages
        //So to illustrate this it is in the page name

        [HttpGet("/Characters/Static-MVCRendered")]
        [HttpGet("/Characters/Server-MVCRendered")]
        [HttpGet("/Characters/ServerPrerendered-MVCRendered")]
        [HttpGet("/Characters/WebAssembly-MVCRendered")]
        [HttpGet("/Characters/WebAssemblyPrerendered-MVCRendered")]

        [HttpPost("/Characters/Static-MVCRendered")]
        [HttpPost("/Characters/Server-MVCRendered")]
        [HttpPost("/Characters/ServerPrerendered-MVCRendered")]
        [HttpPost("/Characters/WebAssembly-MVCRendered")]
        [HttpPost("/Characters/WebAssemblyPrerendered-MVCRendered")]
        [Route("Characters")]
        public async Task<IActionResult> Index()
        {
            CharactersViewModel charactersData = null;
            if (TempData["CharactersData"] != null)
            {
                 charactersData = JsonConvert.DeserializeObject<CharactersViewModel>(TempData["CharactersData"].ToString());
            }
           

            var viewModel = charactersData ?? new CharactersViewModel((await _charactersStateService.GetCharactersAsync()).Data);
            ViewBag.RenderMode = GetRenderModeStr(HttpContext.Request.Path.Value,"Characters");//Returns static if no rendermode so can use /Characters/
            return View(viewModel);
        }

       
    }
}
    

