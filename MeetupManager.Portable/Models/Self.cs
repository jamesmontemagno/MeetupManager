
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
    public class Self
    {
        [JsonProperty("common")]
        public Common Common { get; set; }
    }
}