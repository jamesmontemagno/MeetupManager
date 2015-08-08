
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
	public class Meta
	{
		public Meta ()
		{
		}

		[JsonProperty("lon")]
		public string Longitude { get; set; }

		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("link")]
		public string Link { get; set; }

		[JsonProperty("next")]
		public string Next { get; set; }

		[JsonProperty("total_count")]
		public int TotalCount { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("updated")]
		public long Updated { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("method")]
		public string Method { get; set; }

		[JsonProperty("lat")]
		public string Latitude { get; set; }
	}
}

