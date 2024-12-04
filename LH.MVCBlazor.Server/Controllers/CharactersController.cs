﻿using LH.MVCBlazor.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Package.Shared.BlazorComponents.Enums;
using Package.Shared.Services.StateServices.CharacterStateServices;

namespace LH.MVCBlazor.Server.Controllers
{

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
        public async Task<IActionResult> Index()
        {

            var viewModel = new CharactersViewModel((await _charactersStateService.GetCharactersAsync()).Data);
            ViewBag.RenderMode = GetRenderModeStr();
            return View(viewModel);
        }
        public IActionResult Index(CharactersViewModel CharactersViewModel)
        {

            var viewModel = CharactersViewModel;
            ViewBag.RenderMode = GetRenderModeStr();
            return View(viewModel);
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
    

