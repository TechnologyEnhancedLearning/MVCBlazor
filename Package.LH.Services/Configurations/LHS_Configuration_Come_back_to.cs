using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Package.LH.Services.Configurations.AttendeesConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations
{
    public class LHS_Configuration : ILHS_Configuration    {
   

        public string ClientName { get; set; }
        public string BaseURL { get; set; }
        public LHS_AttendeesAPIEndpointGroup Endpoints { get; set; }

  
    }
}
