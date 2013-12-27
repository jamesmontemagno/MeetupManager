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

