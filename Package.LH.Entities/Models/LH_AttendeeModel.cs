
using Package.Shared.Entities.BaseClasses;
using Package.Shared.Entities.Interfaces.ComponentInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Entities.Models
{
    public class LH_AttendeeModel : GE_PersonBase
    {
  

        // Constructor that takes all necessary parameters
        public LH_AttendeeModel(int id, string firstName, string secondName, bool deleted, string role/*, MeetingModel meeting*/)
            : base(id, firstName, secondName, deleted) // Call to base class constructor
        {
            Role = role;
        }

        public string Role { get; set; } = string.Empty;// Unique to attendees
        // Parameterless constructor for initializing new attendees
        public LH_AttendeeModel() : base(0, string.Empty, string.Empty, false) // Call to base constructor with default values
        {
            Role = string.Empty; // Initialize Role to an empty string
        }
        public override string ToString() => $"{FirstName} {SecondName} - {Role}";
    }
}
