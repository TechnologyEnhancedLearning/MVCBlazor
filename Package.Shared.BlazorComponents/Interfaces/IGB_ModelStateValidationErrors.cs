﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.BlazorComponents.Interfaces
{
    public interface IGB_ModelStateValidationErrors
    {
        //public string AspFor { get; set; } maybeeeee???? qqqq if its always required which it may be
        Dictionary<string, List<string>> ModelStateErrors { get; set; }
        bool HasModelStateValidationErrors { get; }
    }
}
