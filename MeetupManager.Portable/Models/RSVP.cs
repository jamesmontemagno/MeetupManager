using System;
using System.Collections.Generic;

namespace MeetupManager.Portable.Models
{
	public class RSVP
	{
		public RSVP ()
		{
		}
		public string response { get; set; }
		public Member member { get; set; }
		public MemberPhoto member_photo { get; set; }
		public object created { get; set; }
		public Event @event { get; set; }
		public object mtime { get; set; }
		public List<string> answers { get; set; }
		public int guests { get; set; }
		public int rsvp_id { get; set; }
		public Venue venue { get; set; }
		public Group group { get; set; }
	}
}

