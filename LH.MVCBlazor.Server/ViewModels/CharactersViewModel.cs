using Package.LH.Entities.Models;
using Package.Shared.Entities.Models;

namespace LH.MVCBlazor.Server.ViewModels
{
    public class CharactersViewModel
    {
        //Kind of makes sense to be list cartoon qqqq
        public string some_CharactersViewModel_specific_UI_String { get; set; } = "UnSet";
        public List<GE_CharacterModel> Characters { get; set; }
    }
}
