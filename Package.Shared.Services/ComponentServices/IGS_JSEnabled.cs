using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.ComponentServices
{
    public interface IGS_JSEnabled
    {
        public bool JSIsEnabled { get; set; }

        public string TestingWhoAmI { get; set; }
    }
}
