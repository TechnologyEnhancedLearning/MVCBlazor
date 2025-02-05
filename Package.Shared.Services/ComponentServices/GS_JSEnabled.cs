using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.ComponentServices
{
    
    public class GS_JSEnabled : IGS_JSEnabled
    {
        /* 
         * qqqq RENAME REALLY NEEDED

	        - IComponentContext
	        - IRenderConditions?
        - Prerendering? = server rndered
        - JSAvailable? = it will be wasm renderered to disable every post until then
         
         */
        public bool JSIsEnabled { get; set; } = false;
        public string TestingWhoAmI { get; set; } = "Unset";
    }
}
