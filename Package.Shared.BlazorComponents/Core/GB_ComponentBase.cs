using Microsoft.AspNetCore.Components;
using Package.Shared.Services.ComponentServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.Core
{
    public class GB_ComponentBase : ComponentBase
    {
        [Inject]
        private GS_JSEnabled JSEnabled { get; set; } // this will receive server version prerender and then client side if received must be true
        protected bool JSIsEnabled => JSEnabled.JSIsEnabled;

        protected string WhoAmI => JSEnabled.TestingWhoAmI;


    }
}
