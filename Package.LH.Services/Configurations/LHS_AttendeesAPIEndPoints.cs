using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations
{
    public class LHS_AttendeesAPIEndpoints: ILHS_AttendeesAPIEndpoints
    {

        public string LoadAttendees { get; set; } = "UnSet";
        public string ReplaceDBWithList { get; set; } = "UnSet";
        public string ClientName { get; set; } = "UnSet";
    }
}
