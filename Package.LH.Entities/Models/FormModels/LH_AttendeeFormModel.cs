
using Package.Shared.Entities.Interfaces.ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Entities.Models.FormModels
{
    public class LH_AttendeeFormModel : LH_AttendeeModel , IGE_ModelStateValidation
    {
        public Dictionary<string, List<string>> ModelStateErrors { get; set; } = new();

        public bool HasModelStateValidationErrors => ModelStateErrors.Any();

        public LH_AttendeeFormModel() { }
    }
}
