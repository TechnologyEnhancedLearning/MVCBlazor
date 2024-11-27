using Package.Shared.Entities.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Models
{
    public class GE_CartoonModel :GE_GroupBase<GE_CharacterModel>
    {

        public string? Genre { get; set; } = null;


        // Constructor that takes all necessary parameters
        public GE_CartoonModel(int id, string title, bool deleted, List<GE_CharacterModel> characters)
            : base(id, title, deleted, characters) // Call to base class constructor
        {


        }
        public GE_CartoonModel(int id, string title, bool deleted, List<GE_CharacterModel> characters, string genre)
            : base(id, title, deleted, characters) // Call to base class constructor
        {

            Genre = genre;
        }

    }
}
