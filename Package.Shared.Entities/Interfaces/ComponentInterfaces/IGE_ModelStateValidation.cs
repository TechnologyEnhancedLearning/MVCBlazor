using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Interfaces.ComponentInterfaces
{
    public interface IGE_ModelStateValidation
    {
        //public string AspFor { get; set; } maybeeeee???? qqqq if its always required which it may be
        Dictionary<string, List<string>> ModelStateErrors { get; set; }
        bool HasModelStateValidationErrors { get; }
    }
}
