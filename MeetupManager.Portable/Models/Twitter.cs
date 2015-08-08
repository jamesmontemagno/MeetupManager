
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
    public class Twitter
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }
}