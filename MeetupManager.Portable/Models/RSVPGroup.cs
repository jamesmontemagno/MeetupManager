
using System;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
	public class RSVPGroup
	{
        public RSVPGroup()
		{
		}

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("group_lat")]
		public double GroupLatitude { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("group_lon")]
		public double GroupLongitude { get; set; }

		[JsonProperty("join_mode")]
		public string JoinMode { get; set; }

		[JsonProperty("urlname")]
		public string UrlName { get; set; }

		[JsonProperty("who")]
		public string Who { get; set; }
	}
}

