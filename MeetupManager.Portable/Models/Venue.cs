
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
	public class Venue
	{
		public Venue ()
		{
		}

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("zip")]
		public string Zip { get; set; }

		[JsonProperty("lon")]
		public double Longitude { get; set; }

		[JsonProperty("repinned")]
		public bool RePinned { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("address_1")]
		public string Address1 { get; set; }

		[JsonProperty("lat")]
		public double Latitude { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

	}
}

