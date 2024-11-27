using Package.Shared.Entities.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Entities.Models
{
    public class LH_MeetingModel : GE_GroupBase<LH_AttendeeModel>
    {
        public string? Location { get; set; } = null;
        public LH_MeetingModel(int id, string title, bool deleted, List<LH_AttendeeModel> collection)
            : base(id, title, deleted, collection) // Call to base class constructor
        {

        }
        public LH_MeetingModel(int id, string title, bool deleted, List<LH_AttendeeModel> collection, string location)
            : base(id, title, deleted, collection) // Call to base class constructor
        {
            Location = location;
        }
    }
}
