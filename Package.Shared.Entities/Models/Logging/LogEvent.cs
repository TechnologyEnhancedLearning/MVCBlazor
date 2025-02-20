using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Models.Logging
{
    public class LogEvent
    {
        public string Level { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Trace { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
        public string? LogFrom { get; set; } = null; ///client or server whoami js_enabed
        public string? User { get; set; } // Nullable
        public string? Browser { get; set; } // Nullable

        // Session ID (Also included, nullable):
        public string? SessionId { get; set; }
    }
}
