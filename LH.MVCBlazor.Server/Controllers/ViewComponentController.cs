using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Package.Shared.Services.StateServices.CharacterStateServices;

namespace LH.MVCBlazor.Server.Controllers
{
    public class ViewComponentController : Controller
    {
        private readonly IGS_CharactersStateService _charactersStateService;
        public ViewComponentController(IGS_CharactersStateService charactersStateService)
        {
            _charactersStateService = charactersStateService;
        }

        [Route("ViewComponentMVCPage")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new CharactersViewModel { Characters = (await _charactersStateService.GetCharactersAsync()).Data };
            return View(viewModel);
        }
    }
}
