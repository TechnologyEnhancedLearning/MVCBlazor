using Package.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.BaseClasses
{
    public abstract class GE_PersonBase : IGE_Person
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "First name is required.")]
        [MinLength(3, ErrorMessage = "First name must be at least 3 characters long.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Second name is required.")]
        [MinLength(3, ErrorMessage = "Second name must be at least 3 characters long.")]
        [StringLength(50, ErrorMessage = "Second name cannot exceed 50 characters.")]
        public string SecondName { get; set; } = "";

        public bool Deleted { get; set; }
        public Guid ClientTemporaryId { get; set; }

        // Constructor that initializes all properties
        protected GE_PersonBase(int id, string firstName, string secondName, bool deleted)
        {
            Id = id;
            FirstName = firstName;
            SecondName = secondName;
            Deleted = deleted;
            ClientTemporaryId = Guid.NewGuid(); // Automatically generates a new Guid
        }
        public override string ToString() => $"{FirstName} {SecondName}";


     
    }
}
