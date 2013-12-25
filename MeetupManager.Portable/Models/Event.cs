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

