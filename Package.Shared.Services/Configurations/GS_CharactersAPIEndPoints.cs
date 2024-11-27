using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Configurations
{
    public class GS_CharactersAPIEndpoints : IGS_CharactersAPIEndpoints
    {
        public string ClientName { get; set; } = "Unset";
        public string LoadCharacters { get; set; } = "Unset";
        public string SetCharacterAsFavourite { get; set; } = "Unset";

        public string ReplaceDBWithList { get; set; } = "UnSet";
    }
}
