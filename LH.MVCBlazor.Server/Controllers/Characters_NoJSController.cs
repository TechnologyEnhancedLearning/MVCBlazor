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
    
     
    }
}
