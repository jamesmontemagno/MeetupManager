
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
	public class LoggedInUser
	{
		[JsonProperty("lon")]
		public double Longitude { get; set; }
		[JsonProperty("hometown")]
		public string Hometown { get; set; }
		[JsonProperty("status")]
		public string Status { get; set; }
		[JsonProperty("link")]
		public string Link { get; set; }
		[JsonProperty("state")]
		public string State { get; set; }
		[JsonProperty("self")]
		public Self Self { get; set; }
		[JsonProperty("photo")]
		public Photo Photo { get; set; }
		[JsonProperty("lang")]
		public string Langitude { get; set; }
		[JsonProperty("country")]
		public string Country { get; set; }
		[JsonProperty("city")]
		public string City { get; set; }
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("visited")]
		public long Visited { get; set; }
		[JsonProperty("topics")]
		public List<Topic> Topics { get; set; }
		[JsonProperty("joined")]
		public long Joined { get; set; }
		[JsonProperty("bio")]
		public string Bio { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("other_services")]
		public OtherServices OtherServices { get; set; }
		[JsonProperty("lat")]
		public double Lat { get; set; }
	}
}

