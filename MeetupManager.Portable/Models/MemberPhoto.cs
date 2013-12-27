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
	public class MemberPhoto
	{
		public MemberPhoto ()
		{
		}

		[JsonProperty("photo_link")]
		public string PhotoLink { get; set; }

		[JsonProperty("highres_link")]
		public string HighResLink { get; set; }

		[JsonProperty("thumb_link")]
		public string ThumbLink { get; set; }

		[JsonProperty("photo_id")]
		public int PhotoId { get; set; }
	}
}

