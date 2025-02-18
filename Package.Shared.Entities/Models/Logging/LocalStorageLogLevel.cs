using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Models.Logging
{
    public class LocalStorageLogLevel
    {
        public string Level { get; set; } = null;
        public DateTime Expires { get; set; }
    }
}
