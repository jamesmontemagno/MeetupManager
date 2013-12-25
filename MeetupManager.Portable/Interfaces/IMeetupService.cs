using System;
using System.Threading.Tasks;
using MeetupManager.Portable.Services.Responses;

namespace MeetupManager.Portable.Interfaces
{
	public interface IMeetupService
	{
		Task<EventsRootObject> GetEvents(int skip);
		Task<RSVPsRootObject> GetRSVPs (string eventId, int skip);
	}
}

