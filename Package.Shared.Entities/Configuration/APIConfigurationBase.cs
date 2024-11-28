using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Configuration
{
    public class APIConfigurationBase<T> : IAPIConfiguration<T> where T : class
    {
        public string ClientName { get; set; } = "UnSet";
        public string BaseURL { get; set; } = "UnSet";
        public T Endpoints { get; set; }
    }
}
