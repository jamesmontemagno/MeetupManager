
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
	public class Member
	{
		public Member ()
		{
		}
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("member_id")]
		public int MemberId { get; set; }

	}
}

