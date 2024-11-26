using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Configurations
{
    public class GS_CharactersAPIEndPoints : IGS_CharactersAPIEndPoints
    {
        public string ClientName { get; set; } = "Unset";
        public string LoadCartoons { get; set; } = "Unset";
        public string SetCharacterAsFavourite { get; set; } = "Unset";
    }
}
