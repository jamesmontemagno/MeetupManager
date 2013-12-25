using System;

namespace MeetupManager.Portable.Models
{
	public class Event
	{
		public Event ()
		{
		}

		public string status { get; set; }
		public string visibility { get; set; }
		public int maybe_rsvp_count { get; set; }
		public Venue venue { get; set; }
		public string id { get; set; }
		public int utc_offset { get; set; }
		public int duration { get; set; }
		public long time { get; set; }
		public int waitlist_count { get; set; }
		public bool announced { get; set; }
		public long updated { get; set; }
		public int yes_rsvp_count { get; set; }
		public long created { get; set; }
		public string event_url { get; set; }
		public string description { get; set; }
		public string how_to_find_us { get; set; }
		public string name { get; set; }
		public int headcount { get; set; }
		public Group group { get; set; }
	}
}

