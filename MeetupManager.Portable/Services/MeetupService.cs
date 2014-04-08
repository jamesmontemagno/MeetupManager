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
	    private IHttpClientHelper httpClientHelper;
	    public MeetupService(IHttpClientHelper httpClientHelper = null)
	    {
	        this.httpClientHelper = httpClientHelper;
	    }

	    private HttpClient CreateClient()
	    {
	        if(httpClientHelper == null)
                return new HttpClient();

            return new HttpClient(httpClientHelper.MessageHandler);
	    }
		#region IMeetupService implementation

		public static string ClientId = "kgqtisiigj7mpbpbfs1ei7s2h0";
		public static string ClientSecret = "rujr7c9vf8m7hlc6ous2390ni5";
	    public static string AuthorizeUrl = "https://secure.meetup.com/oauth2/authorize";
	    public static string RedirectUrl = "http://www.refractored.com/login_success.html";
	    public static string AccessTokenUrl = "https://secure.meetup.com/oauth2/access";


        private const string GetGroupsUrl = @"https://api.meetup.com/2/groups?offset={0}&member_id={1}&page=100&order=name&access_token={2}&only=name,id,group_photo";
        private const string GetEventsUrl = @"https://api.meetup.com/2/events?offset={0}&group_id={1}&page=100&status=upcoming,past&desc=true&access_token={2}&only=name,id,time";
        private const string GetRSVPsUrl = @"https://api.meetup.com/2/rsvps?offset={0}&event_id={1}&page=100&order=name&rsvp=yes&access_token={2}&only=member,member_photo";
        private const string GetUserUrl = @"https://api.meetup.com/2/member/self?access_token={0}&only=name,id,photo";

		
		public async Task<EventsRootObject> GetEvents (string groupId, int skip)
		{
		    var offset = skip/100;
            if (!await RenewAccessToken())
            {
                Mvx.Resolve<IMessageDialog>().SendToast("Unable to get events, please re-login.");
                return new EventsRootObject() { Events = new List<Event>() };
            }

            var client = CreateClient();
            if (client.DefaultRequestHeaders.CacheControl == null)
                client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue();

            client.DefaultRequestHeaders.CacheControl.NoCache = true;
            client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;
            client.DefaultRequestHeaders.CacheControl.NoStore = true;
            client.Timeout = new TimeSpan(0, 0, 30);
            var request = string.Format(GetEventsUrl, offset, groupId, Settings.AccessToken);
		    if (!Settings.ShowAllEvents)
		        request += "&time=-100m,2m";

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


            var client = CreateClient();
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



        public async Task<RequestTokenObject> GetToken(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return null;

            using (var client = CreateClient())
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
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                        new KeyValuePair<string, string>("redirect_uri", RedirectUrl), 
                        new KeyValuePair<string, string>("code", code), 
                    });

                    var result = await client.PostAsync("https://secure.meetup.com/oauth2/access", content);
                    var response = await result.Content.ReadAsStringAsync();
                    var refreshResponse = await DeserializeObjectAsync<RequestTokenObject>(response);
                    return refreshResponse;
                }
                catch (Exception ex)
                {
                    return null;
                }

            }

            return null;
        }

        public class RequestTokenObject
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }

        public async Task<bool> RenewAccessToken()
		{
			if (string.IsNullOrWhiteSpace (Settings.AccessToken))
				return false;

			if (DateTime.UtcNow < new DateTime(Settings.KeyValidUntil))
				return true;

            using (var client = CreateClient())
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

					var result = await client.PostAsync("https://secure.meetup.com/oauth2/access", content);
					var response = await result.Content.ReadAsStringAsync();
                    var refreshResponse = await DeserializeObjectAsync<RefreshRootObject>(response);
                    Settings.AccessToken = refreshResponse.AccessToken;
					var nextTime = DateTime.UtcNow.AddSeconds(refreshResponse.ExpiresIn).Ticks;
					Settings.KeyValidUntil = nextTime;
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
            var client = CreateClient();
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

            var client = CreateClient();
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
			return Task.Factory.StartNew (() => JsonConvert.DeserializeObject<T> (value));
		}

		public T DeserializeObject<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}
    }
}

