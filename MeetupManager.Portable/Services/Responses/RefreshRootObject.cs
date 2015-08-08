
using Newtonsoft.Json;

namespace MeetupManager.Portable.Services.Responses
{
	public class RefreshRootObject
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
		[JsonProperty("token_type")]
		public string TokenType { get; set; }
		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
	}
}

