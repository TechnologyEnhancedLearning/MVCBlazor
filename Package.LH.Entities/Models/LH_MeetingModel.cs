using Package.Shared.Entities.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Entities.Models
{
    public class LH_MeetingModel : GE_GroupBase
    {
        public string Location { get; set; }
        //collection -> public List<LH_AttendeeModel> Attendees { get; set; } // List of attendees specific to this meeting

        public LH_MeetingModel(int id, string title, bool deleted, List<Type> collection, string location)
            : base(id, title, deleted,collection) // Call to base class constructor
        {
            Location= location;
        }
    }
}
