using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Interfaces.ComponentInterfaces
{
    public interface IGE_ModelStateValidation
    {
       
        Dictionary<string, List<string>> ModelStateErrors { get; set; }
        bool HasModelStateValidationErrors { get; }
    }
}
