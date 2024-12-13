using Package.Shared.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations.AttendeesConfiguration
{

    public class LHS_AttendeesAPIConfiguration : APIConfigurationBase<LHS_AttendeesAPIEndpointGroup>, ILHS_AttendeesAPIConfiguration
    {

        public LHS_AttendeesAPIConfiguration()
        {
            Endpoints = new LHS_AttendeesAPIEndpointGroup();
        }
    }

    public class LHS_AttendeesAPIEndpointGroup : ILHS_AttendeesAPIEndpointGroup
    {
        public LHS_AttendeesAPIEndpoints Attendees { get; set; } = new LHS_AttendeesAPIEndpoints();
    }

    public class LHS_AttendeesAPIEndpoints : ILHS_AttendeesAPIEndpoints
    {
        public string LoadAttendees { get; set; } = "UnSet";
        public string ReplaceDBWithList { get; set; } = "UnSet";
    }
}
