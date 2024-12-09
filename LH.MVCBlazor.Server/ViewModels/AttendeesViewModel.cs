using Package.LH.BlazorComponents.Models;
using Package.LH.Entities.Models;
using Package.LH.Entities.Models.FormModels;
using Package.Shared.Entities.Models;

namespace LH.MVCBlazor.Server.ViewModels
{
    public class AttendeesViewModel
    {
        //qqqq maybe it should be a meeting controller and meeting view
        //LH_MeetingModel LH_MeetingModel { get; set; }

        public string some_AttendeesViewModel_specific_UI_String { get; set; } = "UnSet";
        public List<LH_AttendeeModel> Attendees { get; set; } = null;

        public LH_AttendeeFormModel LH_AttendeeFormModel { get; set; } = new();


        public AttendeesViewModel(List<LH_AttendeeModel> attendees, LH_AttendeeFormModel CurrentFormData = null)
        {
            LH_AttendeeFormModel = CurrentFormData ?? LH_AttendeeFormModel;

            Attendees = attendees;
        }

        public AttendeesViewModel()
        {

        }
    }
}
