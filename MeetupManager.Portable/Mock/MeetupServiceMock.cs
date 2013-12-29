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


		public Task<bool> RenewAccessToken ()
		{
			throw new NotImplementedException ();
		}
		#endregion


	}
}

