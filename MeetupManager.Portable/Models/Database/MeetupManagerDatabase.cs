using System.Collections.Generic;

using MeetupManager.Portable.Interfaces.Database;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

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
