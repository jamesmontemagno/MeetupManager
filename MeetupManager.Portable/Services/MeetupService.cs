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
using System.Collections.Generic;
using System.Net.Http.Headers;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces;
using System.Threading.Tasks;
using MeetupManager.Portable.Services.Responses;
using System.Net.Http;
using Newtonsoft.Json;
using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.Models;

namespace MeetupManager.Portable.Services
{
	public class MeetupService : IMeetupService
	{
		#region IMeetupService implementation


		public static string ClientId = "<YOUR KEY>";
		public static string ClientSecret = "<YOUR SECRET>";

        private const string GetGroupsUrl = @"https://api.meetup.com/2/groups?offset={0}&member_id={1}&page=100&order=name&access_token={2}&only=name,id,group_photo";
        private const string GetEventsUrl = @"https://api.meetup.com/2/events?offset={0}&group_id={1}&page=20&status=upcoming,past&desc=true&access_token={2}&only=name,id,time";
        private const string GetRSVPsUrl = @"https://api.meetup.com/2/rsvps?offset={0}&event_id={1}&page=100&order=name&rsvp=yes&access_token={2}&only=member,member_photo";
        private const string GetUserUrl = @"https://api.meetup.com/2/member/self?access_token={0}&only=name,id,photo";

		
		public async Task<EventsRootObject> GetEvents (string groupId, int skip)
		{
		    var offset = skip/20;
            if (!await RenewAccessToken())
            {
                Mvx.Resolve<IMessageDialog>().SendToast("Unable to get events, please re-login.");
                return new EventsRootObject() { Events = new List<Event>() };
            }

            var client = new HttpClient();
            if (client.DefaultRequestHeaders.CacheControl == null)
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            client.DefaultRequestHeaders.CacheControl.NoCache = true;
            client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            client.DefaultRequestHeaders.CacheControl.NoStore = true;
            client.Timeout = new TimeSpan(0, 0, 30);
            var request = string.Format(GetEventsUrl, offset, groupId, Settings.AccessToken);
            var response = await client.GetStringAsync(request);
			return await DeserializeObjectAsync<EventsRootObject> (response);
		}

		public async Task<RSVPsRootObject> GetRSVPs(string eventId, int skip)
		{
            var offset = skip / 100;
            if (!await RenewAccessToken())
            {
                Mvx.Resolve<IMessageDialog>().SendToast("Unable to get RSVPs, please re-login.");
                return new RSVPsRootObject() { RSVPs = new List<RSVP>() };
            }


			var client = new HttpClient ();
            if (client.DefaultRequestHeaders.CacheControl == null)
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            client.DefaultRequestHeaders.CacheControl.NoCache = true;
            client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            client.DefaultRequestHeaders.CacheControl.NoStore = true;
            client.Timeout = new TimeSpan(0, 0, 30);
			var request = string.Format (GetRSVPsUrl, offset, eventId, Settings.AccessToken);
            var response = await client.GetStringAsync(request);
			return await DeserializeObjectAsync<RSVPsRootObject> (response);

		}

      


        public async Task<bool> RenewAccessToken()
		{
			if (string.IsNullOrWhiteSpace (Settings.AccessToken))
				return false;

			if (DateTime.UtcNow.Ticks < Settings.KeyValidUntil)
				return true;

            using (var client = new HttpClient())
            {
                try
                {
                    if (client.DefaultRequestHeaders.CacheControl == null)
                        client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

                    client.DefaultRequestHeaders.CacheControl.NoCache = true;
                    client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
                    client.DefaultRequestHeaders.CacheControl.NoStore = true;
                    client.Timeout = new TimeSpan(0, 0, 30);
          
                    var content = new FormUrlEncodedContent(new[] 
                    {
                        new KeyValuePair<string, string>("client_id", ClientId),
                        new KeyValuePair<string, string>("client_secret", ClientSecret),
                        new KeyValuePair<string, string>("grant_type", "refresh_token"),
                        new KeyValuePair<string, string>("refresh_token", Settings.RefreshToken), 
                    });

                    var result = client.PostAsync("https://secure.meetup.com/oauth2/access", content).Result;
                    var response = result.Content.ReadAsStringAsync().Result;
                    var refreshResponse = await DeserializeObjectAsync<RefreshRootObject>(response);
                    Settings.AccessToken = refreshResponse.AccessToken;
                    Settings.KeyValidUntil = DateTime.UtcNow.Ticks + refreshResponse.ExpiresIn;
                    Settings.RefreshToken = refreshResponse.RefreshToken;
                }
                catch (Exception ex)
                {
                    return false;
                }
                
            }

			return true;
		}


		public async Task<LoggedInUser> GetCurrentMember ()
		{
			await RenewAccessToken();
            var client = new HttpClient();
            if (client.DefaultRequestHeaders.CacheControl == null)
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            client.DefaultRequestHeaders.CacheControl.NoCache = true;
            client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            client.DefaultRequestHeaders.CacheControl.NoStore = true;
            client.Timeout = new TimeSpan(0, 0, 30);
			var request = string.Format (GetUserUrl, Settings.AccessToken);
			var response = await client.GetStringAsync (request);
		  
            //should use async, but has issue for some reason and throws exception
		    return DeserializeObject<LoggedInUser>(response);
		        
		}
		#endregion




        public async Task<GroupsRootObject> GetGroups(string memberId, int skip)
        {
            var offset = skip / 100;
            if(!await RenewAccessToken())
            {
                Mvx.Resolve<IMessageDialog>().SendToast("Unable to get groups, please re-login.");
                return new GroupsRootObject{Groups = new List<Group>()};
            }

            var client = new HttpClient();
            if (client.DefaultRequestHeaders.CacheControl == null)
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            client.DefaultRequestHeaders.CacheControl.NoCache = true;
            client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            client.DefaultRequestHeaders.CacheControl.NoStore = true;
            client.Timeout = new TimeSpan(0, 0, 30);
            var request = string.Format(GetGroupsUrl, offset, memberId, Settings.AccessToken);

            var response = await client.GetStringAsync(request);
            return await DeserializeObjectAsync<GroupsRootObject>(response);
        }

		public  Task<T> DeserializeObjectAsync<T>(string value)
		{
			if (Mvx.CanResolve<IDeserialize> ())
				return Mvx.Resolve<IDeserialize> ().DeserializeObjectAsync<T> (value);

			return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(value));
		}

		public T DeserializeObject<T>(string value)
		{
			if (Mvx.CanResolve<IDeserialize> ())
				return Mvx.Resolve<IDeserialize> ().DeserializeObject<T> (value);

			return JsonConvert.DeserializeObject<T>(value);
		}
    }
}

