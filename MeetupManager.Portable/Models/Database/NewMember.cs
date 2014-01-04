using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace MeetupManager.Portable.Models.Database
{
    public class NewMember: BusinessEntityBase
	{
		public NewMember ()
		{
		}

        public NewMember(string eventId, string name)
		{
			EventId = eventId;
            Name = name;
		}


		[Indexed]
		public string EventId { get; set; }

        public string Name { get; set; }
	}
}
