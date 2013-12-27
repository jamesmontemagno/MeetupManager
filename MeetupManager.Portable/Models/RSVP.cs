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
	public class RSVP
	{
		public RSVP ()
		{
		}
		[JsonProperty("response")]
		public string Response { get; set; }

		[JsonProperty("member")]
		public Member Member { get; set; }

		[JsonProperty("member_photo")]
		public MemberPhoto MemberPhoto { get; set; }

		[JsonProperty("created")]
		public object Created { get; set; }

		[JsonProperty("event")]
		public Event Event { get; set; }

		[JsonProperty("mtime")]
		public object MTime { get; set; }

		[JsonProperty("answers")]
		public List<string> Answers { get; set; }

		[JsonProperty("guests")]
		public int Guests { get; set; }

		[JsonProperty("rsvp_id")]
		public int RSVPId { get; set; }

		[JsonProperty("venue")]
		public Venue Venue { get; set; }

		[JsonProperty("group")]
		public Group Group { get; set; }
	}
}

