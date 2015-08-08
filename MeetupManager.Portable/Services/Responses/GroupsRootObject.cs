
using System.Collections.Generic;
using MeetupManager.Portable.Models;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Services.Responses
{
    public class GroupsRootObject
    {
        [JsonProperty("results")]
        public List<Group> Groups { get; set; }
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
