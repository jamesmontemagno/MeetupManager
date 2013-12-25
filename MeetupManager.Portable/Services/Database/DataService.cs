using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Models.Database;
using System.Threading.Tasks;

namespace MeetupManager.Portable.Services.Database
{
    /// <summary>
    /// Is the main data service that can be used in the application for reading/writing to the cameroon database.
    /// </summary>
    public class DataService : IDataService
    {
		private readonly MeetupManagerDatabase database;
        public DataService(ISQLiteConnectionFactory factory)
        {
			this.database = new MeetupManagerDatabase(factory);
        }

		#region IDataService implementation

		public async Task CheckInMember (EventRSVP rsvp)
		{
			await Task.Factory.StartNew (() => {
				database.SaveItem<EventRSVP> (rsvp);
			});
		}

		public async Task<bool> IsCheckedIn (string eventId, string userId)
		{
			return await Task.Factory.StartNew<bool> (() => {
				return database.GetEventRSVP(eventId, userId) != null;
			});
		}

		#endregion
    }
}
