using Package.Shared.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations.AttendeesConfiguration
{
    public interface ILHS_AttendeesAPIConfiguration : IAPIConfiguration<LHS_AttendeesAPIEndpointGroup>
    {
        // Marker interface with no additional members
    }

    public interface ILHS_AttendeesAPIEndpointGroup
    {
        public LHS_AttendeesAPIEndpoints Attendees { get; set; }
    }

    public interface ILHS_AttendeesAPIEndpoints
    {
        public string LoadAttendees { get; set; }
        public string ReplaceDBWithList { get; set; }
    }
}
