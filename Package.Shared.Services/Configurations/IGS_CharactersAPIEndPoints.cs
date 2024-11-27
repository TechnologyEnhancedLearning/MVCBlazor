using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Configurations
{
    public interface IGS_CharactersAPIEndpoints
    {
        public string ClientName { get; set; }
        public string LoadCharacters { get; set; }

        public string ReplaceDBWithList { get; set; }
        public string SetCharacterAsFavourite { get; set; }
    }
}
