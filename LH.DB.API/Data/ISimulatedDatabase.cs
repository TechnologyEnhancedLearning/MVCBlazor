using Package.LH.Entities.Models;
using Package.Shared.Entities.BaseClasses;
using Package.Shared.Entities.Interfaces;
using Package.Shared.Entities.Models;

namespace LH.DB.API.Data
{
    public interface ISimulatedDatabase
    {
        // Property to access the list of cartoons
        List<GE_CartoonModel> Cartoons { get; }

        // Property to access the list of meetings
        List<LH_MeetingModel> Meetings { get; }

        public void AddAttendeeToMeeting(int meetingId, LH_AttendeeModel attendee);
        public void AddCharacterToCartoon(int cartoonId, GE_CharacterModel character);

        public void ReassignListGroupPeopleIds<T>(List<GE_GroupBase<T>> groups) where T : IGE_Person;

        public string ClickCount { get; set; }
    }
}
