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
