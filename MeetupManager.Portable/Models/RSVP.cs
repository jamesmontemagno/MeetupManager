
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
	public class RSVP
	{
		public RSVP ()
		{
		}
		[JsonProperty("response")]
		public string Response { get; set; }

		[JsonProperty("member")]
		public Member Member { get; set; }

		[JsonProperty("member_photo")]
		public MemberPhoto MemberPhoto { get; set; }

		[JsonProperty("created")]
		public object Created { get; set; }

		[JsonProperty("event")]
		public Event Event { get; set; }

		[JsonProperty("mtime")]
		public object MTime { get; set; }

		[JsonProperty("answers")]
		public List<string> Answers { get; set; }

		[JsonProperty("guests")]
		public int Guests { get; set; }

		[JsonProperty("rsvp_id")]
		public int RSVPId { get; set; }

		[JsonProperty("venue")]
		public Venue Venue { get; set; }

		[JsonProperty("group")]
		public RSVPGroup Group { get; set; }
	}
}

