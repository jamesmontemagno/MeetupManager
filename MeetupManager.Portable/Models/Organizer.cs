using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
    public class Organizer
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("member_id")]
        public int MemberId { get; set; }
    }
}
