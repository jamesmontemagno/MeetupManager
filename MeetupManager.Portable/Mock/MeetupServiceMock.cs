
using System;
using MeetupManager.Portable.Interfaces;
using System.Threading.Tasks;
using MeetupManager.Portable.Services.Responses;
using MeetupManager.Portable.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using MeetupManager.Portable.Mock;

//[assembly:Dependency(typeof(MeetupServiceMock))]
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
						ThumbLink =  "http://photos1.meetupstatic.com/photos/member/4/3/8/2/member_165857282.jpeg"
					}
				});
			}
			return rsvps;
		}


		public Task<bool> RenewAccessToken ()
		{
			throw new NotImplementedException ();
		}

		public async Task<LoggedInUser> GetCurrentMember ()
		{
			return new LoggedInUser
			{
			    Name = "James Montemagno",
                Id = 1
			};
		}
		#endregion



        public async Task<EventsRootObject> GetEvents(string groupId, int skip)
        {
            var events = new EventsRootObject();
            events.Metadata = new Meta();
            events.Events = new List<Event>();
            for (int i = 0; i < 10; i++)
            {
                events.Events.Add(new Event
                {
                   Name = "Event name : " + i,
                   Id = i.ToString()
                });
            }

            return events;
        }

        public async Task<GroupsRootObject> GetGroups(string memberId, int skip)
        {
            var groups = new GroupsRootObject();
            groups.Meta = new Meta();
            groups.Groups = new List<Group>();

            for (int i = 0; i < 14; i++)
            {
                groups.Groups.Add(new Group
                    {
                        Id = i,
                        Name = "Mock group " + i,
                        GroupPhoto = new GroupPhoto { PhotoLink = "http://photos1.meetupstatic.com/photos/event/3/b/6/2/global_318975202.jpeg", PhotoId = 1 }
                    });
            }

            return groups;
        }


        public Task<Services.MeetupService.RequestTokenObject> GetToken(string code)
        {
            throw new NotImplementedException();
        }
    }
}

