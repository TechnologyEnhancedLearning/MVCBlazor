using Package.Shared.Entities.Configuration;
using Package.Shared.Services.Configuration.CharactersConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Configuration.CounterConfiguration
{
    public interface IT_CounterAPIConfiguration : IAPIConfiguration<T_CounterAPIEndpointGroup>
    {
    }
        public interface IT_CounterAPIEndPointGroup
        {
            public T_CounterAPIEndpoints Counter { get; set; }
        }

        public interface IT_CounterAPIEndpoints
        {

            public string SaveCountToDB { get; set; }
            public string GetCountFromDB { get; set; }

        }
    
    }

