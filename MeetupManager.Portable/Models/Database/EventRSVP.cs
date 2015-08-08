
using SQLite;

namespace MeetupManager.Portable.Models.Database
{
    public class EventRSVP : BusinessEntityBase
    {
        public EventRSVP()
        {
        }

        public EventRSVP(string eventId, string userId, string eventName, string groupId, string groupName, long eventDate)
        {
            EventId = eventId;
            UserId = userId;
            EventName = eventName;
            GroupId = groupId;
            GroupName = groupName;
            EventDate = eventDate;
        }

        public string UserId { get; set; }

        [Indexed]
        public string EventId { get; set; }

        public string EventName { get; set; }

        [Indexed]
        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public long EventDate { get; set; }
    }
}

