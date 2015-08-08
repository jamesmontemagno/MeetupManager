
using System.Threading.Tasks;
using MeetupManager.Portable.Services;
using MeetupManager.Portable.Services.Responses;
using MeetupManager.Portable.Models;

namespace MeetupManager.Portable.Interfaces
{
	public interface IMeetupService
	{
		Task<EventsRootObject> GetEvents(string groupId, int skip);
		Task<RSVPsRootObject> GetRSVPs (string eventId, int skip);
	    Task<GroupsRootObject> GetGroups(string memberId, int skip);
	    Task<MeetupService.RequestTokenObject> GetToken(string code);
		Task<bool> RenewAccessToken ();

		Task<LoggedInUser> GetCurrentMember ();
	}
}

