using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.Core
{
    public class NHSBlazorComponentBase : ComponentBase
    {
        [Inject]
        private IJSEnabled JSEnabled { get; set; } // this will receive server version prerender and then client side if received must be true
        protected bool JSIsEnabled => JSEnabled.JSIsEnabled;

        protected string WhoAmI => JSEnabled.TestingWhoAmI;


    }
}
