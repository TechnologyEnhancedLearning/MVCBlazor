using Package.LH.Entities.Models;
using Package.Shared.Entities.Models;

namespace LH.DB.API.Data
{
    public interface ISimulatedDatabase
    {
        // Property to access the list of cartoons
        List<GE_CartoonModel> Cartoons { get; }

        // Property to access the list of meetings
        List<LH_MeetingModel> Meetings { get; }


    }
}
