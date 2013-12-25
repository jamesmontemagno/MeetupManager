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

