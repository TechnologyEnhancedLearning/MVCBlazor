using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.Core
{
    public abstract class GB_PageBase :GB_ComponentBase
    {
        public abstract string PageTitle { get; set; }

        protected override void OnInitialized()
        {
            // Any additional initialization for your page can be done here
        }
    }
}

