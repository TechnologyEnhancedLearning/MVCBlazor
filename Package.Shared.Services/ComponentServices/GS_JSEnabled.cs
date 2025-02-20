using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.ComponentServices
{
    /// <summary>
    /// This should be renamed in future
    /// IComponentRenderInfo maybe be a better name
    /// </summary>
    public class GS_JSEnabled : IGS_JSEnabled
    {

        public bool JSIsEnabled { get; set; } = false;
        public string TestingWhoAmI { get; set; } = "Unset";
    }
}
