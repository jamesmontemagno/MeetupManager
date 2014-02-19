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
using System.Collections.Generic;

using MeetupManager.Portable.Interfaces.Database;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;
using System.Linq;

namespace MeetupManager.Portable.Models.Database
{

	public class MeetupManagerDatabase
    {
        private readonly ISQLiteConnection m_Connection;
// ReSharper disable InconsistentNaming
		private const string SQLiteDataBaseLocation = "meetupmanager.db3";
// ReSharper restore InconsistentNaming

        private static readonly object Locker = new object();

		public MeetupManagerDatabase(ISQLiteConnectionFactory factory)
        {
            this.m_Connection = factory.Create(SQLiteDataBaseLocation);
            //create taables
			this.m_Connection.CreateTable<EventRSVP>();
		    this.m_Connection.CreateTable<NewMember>();
        }

		/// <summary>
		/// Gets all items of type T
		/// </summary>
		/// <typeparam name="T">Type of item to get</typeparam>
		/// <returns></returns>
		public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
		{
			lock (Locker)
			{
				return (from i in this.m_Connection.Table<T>() select i);
			}
		}

        /// <summary>
        /// Gets all items of type T
        /// </summary>
        /// <typeparam name="T">Type of item to get</typeparam>
        /// <returns></returns>
		public EventRSVP GetEventRSVP(string eventId, string userId)
        {
            lock (Locker)
            {
				var items = (from i in this.m_Connection.Table<EventRSVP> ()
				        where i.UserId == userId && i.EventId == eventId
				        select i);

				if (items.Any())
					return items.ElementAt(0);

				return null;
            }
        }

		public IEnumerable<EventRSVP> GetRSVPsByDate(string groupId)
		{
			return  (from i in this.m_Connection.Table<EventRSVP> ()
				where i.GroupId == groupId
			         select i).OrderBy (n => n.EventDate);
		}

		public IEnumerable<NewMember> GetNewMembersByDate(string groupId)
		{
			return  (from i in this.m_Connection.Table<NewMember> ()
				where i.GroupId == groupId
				select i).OrderBy (n => n.EventDate);
		}

        /// <summary>
        /// Gets all items of type T
        /// </summary>
        /// <typeparam name="T">Type of item to get</typeparam>
        /// <returns></returns>
        public IEnumerable<NewMember> GetNewMembers(string eventId)
        {
            lock (Locker)
            {
                var items = (from i in this.m_Connection.Table<NewMember>()
                             where i.EventId == eventId
                             select i);

                return items;
            }
        }

        /// <summary>
        /// Gets a specific item of type T with specified ID
        /// </summary>
        /// <typeparam name="T">Type of item to get</typeparam>
        /// <param name="id">ID to get</param>
        /// <returns>Item type T or null.</returns>
        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return (from i in this.m_Connection.Table<T>()
                        where i.Id == id
                        select i).FirstOrDefault();
            }
        }

        /// <summary>
        /// Save and update item of type T. If ID is set then will update the item, eelse creates new and returns the id.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="item">Item to save or update</param>
        /// <returns>ID of item</returns>
        public int SaveItem<T>(T item) where T : IBusinessEntity
        {
            lock (Locker)
            {
                if (item.Id != 0)
                {
					this.m_Connection.Update(item);
                    return item.Id;
                }
                
                return this.m_Connection.Insert(item);
            }
        }

        /// <summary>
        /// Saves a list of items calling SaveItems in 1 transaction
        /// </summary>
        /// <typeparam name="T">Type of entity to save</typeparam>
        /// <param name="items">List of items</param>
        public void SaveItems<T>(IEnumerable<T> items) where T : IBusinessEntity
        {
            lock (Locker)
            {
                this.m_Connection.BeginTransaction();

                foreach (T item in items)
                {
                    this.SaveItem(item);
                }

                this.m_Connection.Commit();
            }
        }

        /// <summary>
        /// Deletes a specific item with id specified
        /// </summary>
        /// <typeparam name="T">Type of item to delete</typeparam>

        /// <param name="item"></param>
        /// <returns></returns>
        public int DeleteItem<T>(T item) where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return this.m_Connection.Delete(item);
            }
        }



        /// <summary>
        /// Deletes a specific item with id specified
        /// </summary>
        /// <typeparam name="T">Type of item to delete</typeparam>
        /// <param name="id">Item to delete</param>
        /// <returns></returns>
        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return this.m_Connection.Delete<T>(id);
            }
        }

        /// <summary>
        /// Clear an entire table of specific type
        /// </summary>
        /// <typeparam name="T">Type to clear table</typeparam>
        public void ClearTable<T>() where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                this.m_Connection.Execute(string.Format("delete from \"{0}\"", typeof(T).Name));
            }
        }
    }
}
