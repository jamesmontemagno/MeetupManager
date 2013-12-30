/*
 * MeetupManager:
 * Copyright (C) 2013 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Models
{
    public class Group
    {
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        [JsonProperty("organizer")]
        public Organizer Organizer { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("join_mode")]
        public string JoinMode { get; set; }
        [JsonProperty("who")]
        public string Who { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("category")]
        public Category category { get; set; }

        [JsonProperty("topics")]
        public List<Topic> Topics { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("created")]
        public object Created { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("rating")]
        public double Rating { get; set; }
        [JsonProperty("urlname")]
        public string UrlName { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("members")]
        public int Members { get; set; }

        [JsonProperty("group_photo")]
        public GroupPhoto GroupPhoto { get; set; }
    }
}
