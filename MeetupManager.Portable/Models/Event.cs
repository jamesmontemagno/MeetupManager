using System;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
	public class Event
	{
		public Event ()
		{
		}

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("visibility")]
		public string Visibility { get; set; }

		[JsonProperty("maybe_rsvp_count")]
		public int MaybeRSVPCount { get; set; }

		[JsonProperty("venue")]
		public Venue Venue { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("utc_offset")]
		public int UTCOffset { get; set; }

		[JsonProperty("duration")]
		public int Duration { get; set; }

		[JsonProperty("time")]
		public long Time { get; set; }

		[JsonIgnore]
		public string Month 
		{
			get 
			{ 
				return new TimeSpan (0, 0, 0, (int)Time).ToString("mm"); 
			} 
		}

		[JsonIgnore]
		public string Year 
		{
			get 
			{
				//var span = new DateTime (Time);
				//var year = span.ToString("yyyy");
				return "2013";
			} 
		}

		[JsonIgnore]
		public string Day { get { return new TimeSpan (0, 0, 0, (int)Time).ToString ("dd"); } }

		[JsonIgnore]
		public string MonthDay { get { return "12/25";/*new TimeSpan (0, 0, 0, (int)Time).ToString("mm/dd");*/ } }

		[JsonProperty("waitlist_count")]
		public int WaitlistCount { get; set; }

		[JsonProperty("announced")]
		public bool Announced { get; set; }

		[JsonProperty("updated")]
		public long Updated { get; set; }

		[JsonProperty("yes_rsvp_count")]
		public int YesRSVPCount { get; set; }

		[JsonProperty("created")]
		public long Created { get; set; }

		[JsonProperty("event_url")]
		public string EventUrl { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("how_to_find_us")]
		public string HowToFindUs { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("headcount")]
		public int HeadCount { get; set; }

		[JsonProperty("group")]
		public Group Group { get; set; }
	}
}

