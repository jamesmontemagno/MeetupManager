using System;
using MeetupManager.Portable.Interfaces;
using System.Threading.Tasks;
using MeetupManager.Portable.Services.Responses;
using MeetupManager.Portable.Models;
using System.Collections.Generic;

namespace MeetupManager.Portable.Mock
{
	public class MeetupServiceMock : IMeetupService
	{
		#region IMeetupService implementation


		public async Task<EventsRootObject> GetEvents (int skip)
		{
			var events = new EventsRootObject ();
			events.Metadata = new Meta ();
			events.Events = new List<Event> ();

			for (int i = 0; i < 10; i++) {
				events.Events.Add (new Event {
					Name = "This is the name of an event " + i,
					Time = DateTime.Now.Ticks,
					Id = i.ToString()
				});
			}

			return events;

		}

		public async Task<RSVPsRootObject> GetRSVPs(string eventId, int skip)
		{
			var rsvps = new RSVPsRootObject ();
			rsvps.Metadata = new Meta ();
			rsvps.RSVPs = new List<RSVP> ();
			for (int i = 0; i < 10; i++) {
				rsvps.RSVPs.Add (new  RSVP {
					Member = new Member{
						Name = "This is a name " + i,
						MemberId = i
					},
					MemberPhoto =  new MemberPhoto{
						ThumbLink =  string.Empty
					}
				});
			}
			return rsvps;
		}

		#endregion


	}
}

