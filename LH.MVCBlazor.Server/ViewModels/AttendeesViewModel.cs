using Package.LH.Entities.Models;

namespace LH.MVCBlazor.Server.ViewModels
{
    public class AttendeesViewModel
    {
        //qqqq maybe it should be a meeting controller and meeting view
        //LH_MeetingModel LH_MeetingModel { get; set; }

        public string some_AttendeesViewModel_specific_UI_String { get; set; } = "UnSet";
        public List<LH_AttendeeModel> Attendees { get; set; }
    }
}
