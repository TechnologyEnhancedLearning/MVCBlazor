using Package.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.BaseClasses
{
    public abstract class GE_PersonBase : IGE_Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
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
