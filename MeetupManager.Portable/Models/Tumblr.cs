
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
    public class Tumblr
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
    }
}