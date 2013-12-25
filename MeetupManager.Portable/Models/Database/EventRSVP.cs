using System;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace MeetupManager.Portable.Models.Database
{
	public class EventRSVP : BusinessEntityBase
	{
		public EventRSVP ()
		{
		}

		public string UserId { get; set; }

		[Indexed]
		public string EventId { get; set; }
	}
}

