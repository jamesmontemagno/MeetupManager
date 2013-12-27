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
	public class Venue
	{
		public Venue ()
		{
		}

		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("zip")]
		public string Zip { get; set; }

		[JsonProperty("lon")]
		public double Longitude { get; set; }

		[JsonProperty("repinned")]
		public bool RePinned { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("address_1")]
		public string Address1 { get; set; }

		[JsonProperty("lat")]
		public double Latitude { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

	}
}

