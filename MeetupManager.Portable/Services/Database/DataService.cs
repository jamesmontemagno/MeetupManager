using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Models.Database;

namespace MeetupManager.Portable.Services.Database
{
    /// <summary>
    /// Is the main data service that can be used in the application for reading/writing to the cameroon database.
    /// </summary>
    public class DataService : IDataService
    {
		private readonly MeetupManagerDatabase m_Database;
        public DataService(ISQLiteConnectionFactory factory)
        {
			this.m_Database = new MeetupManagerDatabase(factory);
        }
    }
}
