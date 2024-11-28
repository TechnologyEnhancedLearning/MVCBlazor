using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Configuration
{
    public interface IAPIConfiguration<T> where T : class
    {
        public string ClientName { get; set; }
        public string BaseURL { get; set; }
        public T Endpoints { get; set; }
        //public object Endpoints { get; set; }
    }
}
