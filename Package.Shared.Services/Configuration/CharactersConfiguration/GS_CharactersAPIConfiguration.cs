using Package.Shared.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Services.Configuration.CharactersConfiguration
{
    public class GS_CharactersAPIConfiguration : APIConfigurationBase<GS_CharactersAPIEndpointGroup>, IGS_CharactersAPIConfiguration
    {
       

        public GS_CharactersAPIConfiguration()
        {
            Endpoints = new GS_CharactersAPIEndpointGroup();
        }
    }

    public class GS_CharactersAPIEndpointGroup : IGS_CharactersAPIEndpointGroup
    {
        public GS_CharactersAPIEndpoints Characters { get; set; } = new GS_CharactersAPIEndpoints();
    }

    public class GS_CharactersAPIEndpoints : IGS_CharactersAPIEndpoints
    {
        public string LoadCharacters { get; set; } = "Unset";
        public string SetCharacterAsFavourite { get; set; } = "Unset";

        public string ReplaceDBWithList { get; set; } = "UnSet";
    }
}
