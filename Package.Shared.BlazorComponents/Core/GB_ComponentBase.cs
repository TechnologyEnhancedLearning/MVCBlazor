using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Package.Shared.Services.ComponentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Package.Shared.BlazorComponents.Core
{
    public class GB_ComponentBase : ComponentBase
    {
        [Inject]
        private IGS_JSEnabled JSEnabled { get; set; } // this will receive server version prerender and then client side if received must be true


        [Inject]
        public ILogger<GB_ComponentBase> Logger { get; set; }

        protected bool JSIsEnabled => JSEnabled.JSIsEnabled;

        protected string WhoAmI => JSEnabled.TestingWhoAmI;

        //We could also pass the renderMode here and if it is Static (which we may not decide to use) then we could return JSEnableAndNotStatic
        //so static pages receive post logic

        protected override void OnInitialized()
        {
            base.OnInitialized();
            Logger.LogInformation($"base component made by {WhoAmI}", WhoAmI);

        }


    }
}
