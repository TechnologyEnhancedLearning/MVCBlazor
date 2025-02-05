using Package.Shared.Entities.Configuration;
using Package.Shared.Services.Configuration.CharactersConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Package.Shared.Services.Configuration.CounterConfiguration.IT_CounterAPIConfiguration;

namespace Package.Shared.Services.Configuration.CounterConfiguration
{
    public class T_CounterAPIConfiguration : APIConfigurationBase<T_CounterAPIEndpointGroup>, IT_CounterAPIConfiguration
    {
        public T_CounterAPIConfiguration()
        {
            Endpoints = new T_CounterAPIEndpointGroup();
        }
    }

  
    public class T_CounterAPIEndpointGroup : IT_CounterAPIEndPointGroup
    {
        public T_CounterAPIEndpoints Counter { get; set; } = new T_CounterAPIEndpoints();
    }

    public class T_CounterAPIEndpoints : IT_CounterAPIEndpoints
    {
        public string SetCountInDB { get ; set ; }
        public string GetCountFromDB { get ; set ; }
    }
}
