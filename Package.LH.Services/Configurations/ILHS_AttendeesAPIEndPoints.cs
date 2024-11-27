using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations
{
    public interface ILHS_AttendeesAPIEndpoints
    {
        public string ClientName { get; set; }
        public string LoadAttendees { get; set; }
        public string ReplaceDBWithList { get; set; }

    }
}
