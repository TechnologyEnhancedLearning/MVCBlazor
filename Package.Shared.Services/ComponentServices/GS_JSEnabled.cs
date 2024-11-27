using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.ComponentServices
{
    //qqqq this is only relevant to blazor components so shouldnt it be there, or generic ijsenable and lh the concretion
    public class GS_JSEnabled : IGS_JSEnabled
    {
        public bool JSIsEnabled { get; set; } = false;
        public string TestingWhoAmI { get; set; } = "Unset";
    }
}
