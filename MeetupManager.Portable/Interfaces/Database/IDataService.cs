
using System.Collections.Generic;
using MeetupManager.Portable.Models.Database;
using System.Threading.Tasks;

namespace MeetupManager.Portable.Interfaces.Database
{
    /// <summary>
    /// Data service intrface for all interactions with the database.
    /// </summary>
    public interface IDataService
    {
		Task CheckInMember (EventRSVP rsvp);
        Task CheckOutMember(string eventId, string userId);
        Task<bool> IsCheckedIn(string eventId, string userId, string eventName, string groupId, string groupName, long eventDate);

        Task AddNewMember(NewMember member);
        Task<IEnumerable<NewMember>> GetNewMembers(string eventId);
        Task RemoveNewMember(int id);
		Task<IEnumerable<NewMember>> GetNewMembersForGroup (string groupId);
		Task<IEnumerable<EventRSVP>> GetRSVPsForGroup (string groupId);
    }
}
