using Package.Shared.Entities.BaseClasses;
using Package.Shared.Entities.Interfaces;
using Package.Shared.Entities.Interfaces.ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.Models
{
    public class GE_CharacterModel : GE_PersonBase
    {

        //Character
        public bool IsFavourite { get; set; } = false; // Unique to characters
        public string ImageUrl { get; set; } = "UnSet";  // Unique to characters

        // Constructor that takes all necessary parameters
        public GE_CharacterModel(int id, string firstName, string secondName, bool deleted, bool isFavourite, string imageUrl)
            : base(id, firstName, secondName, deleted) // Call to base class constructor
        {
            IsFavourite = isFavourite;
            ImageUrl = imageUrl;
        }

        // Parameterless constructor for initializing new attendees
        public GE_CharacterModel() : base(0, string.Empty, string.Empty, false) // Call to base constructor with default values
        {

        }
        public override string ToString() => $"{FirstName} {SecondName} - {(IsFavourite? "😄" : "👍")}";
    }
}
