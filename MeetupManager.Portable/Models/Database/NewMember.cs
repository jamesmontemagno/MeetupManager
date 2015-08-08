using SQLite;

namespace MeetupManager.Portable.Models.Database
{
    public class NewMember: BusinessEntityBase
    {
        public NewMember()
        {
        }

        public NewMember(string eventId, string name, string eventName, string groupId, string groupName, long eventDate)
        {
            EventId = eventId;
            Name = name;
            EventName = eventName;
            GroupId = groupId;
            GroupName = groupName;
            EventDate = eventDate;
        }


        [Indexed]
        public string EventId { get; set; }

        public string Name { get; set; }

        public string EventName { get; set; }

        [Indexed]
        public string GroupId { get; set; }

        public string GroupName { get; set; }

        public long EventDate { get; set; }
	
    }
}
