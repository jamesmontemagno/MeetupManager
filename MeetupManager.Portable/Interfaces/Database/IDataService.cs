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
		Task<bool> IsCheckedIn(string eventId, string userId);
    }
}
