using Package.Shared.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Configuration.CharactersConfiguration
{

    public interface IGS_CharactersAPIConfiguration : IAPIConfiguration<GS_CharactersAPIEndpointGroup>
    {
        // Marker interface with no additional members
    }

    public interface IGS_CharactersAPIEndpointGroup
    {
        public GS_CharactersAPIEndpoints Characters { get; set; }
    }

    public interface IGS_CharactersAPIEndpoints
    {
      
        public string LoadCharacters { get; set; }
        public string SetCharacterAsFavourite { get; set; } 
        public string ReplaceDBWithList { get; set; }
    }
}
