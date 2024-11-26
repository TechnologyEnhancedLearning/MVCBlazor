using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Configurations
{
    public interface IGS_CharactersAPIEndPoints
    {
        public string ClientName { get; set; }
        public string LoadCartoons { get; set; }
        public string SetCharacterAsFavourite { get; set; }
    }
}
