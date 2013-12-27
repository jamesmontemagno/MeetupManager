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
using System;
using MeetupManager.Portable.Interfaces;
using System.Threading.Tasks;
using MeetupManager.Portable.Services.Responses;
using System.Net.Http;
using Newtonsoft.Json;

namespace MeetupManager.Portable.Services
{
	public class MeetupService : IMeetupService
	{
		#region IMeetupService implementation

		//private const string GroupUrlName = "<YOUR GROUP NAME HERE>";
		private const string GroupUrlName = "SeattleMobileDevelopers";
		//private const string ApiKey = "&key=<YOUR API KEY HERE>";
		private const string ApiKey = "&key=365b4c1a4567207641623f61775c6";
		private const string GetEventsUrl = @"http://api.meetup.com/2/events?group_urlname=" + GroupUrlName + "&status=upcoming,past" + ApiKey;
		private const string GetRSVPsUrl = @"https://api.meetup.com/2/rsvps?event_id={0}" + ApiKey;

		public async Task<EventsRootObject> GetEvents (int skip)
		{
			var httpClient = new HttpClient ();
			var response = await httpClient.GetStringAsync (GetEventsUrl);
			return await JsonConvert.DeserializeObjectAsync<EventsRootObject> (response);
		}

		public async Task<RSVPsRootObject> GetRSVPs(string eventId, int skip)
		{
			var httpClient = new HttpClient ();
			var response = await httpClient.GetStringAsync (string.Format (GetRSVPsUrl, eventId));
			return await JsonConvert.DeserializeObjectAsync<RSVPsRootObject> (response);

		}

		#endregion


	}
}

