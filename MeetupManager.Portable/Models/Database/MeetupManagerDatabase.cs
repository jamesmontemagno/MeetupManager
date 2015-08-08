
using System.Collections.Generic;

using MeetupManager.Portable.Interfaces.Database;
using System.Linq;
using SQLite;
using System.IO;

namespace MeetupManager.Portable.Models.Database
{

    public class MeetupManagerDatabase
    {
        const string location = "meetupmanager.db3";
        public static string Root {get;set;}
        SQLiteConnection Connection { get; }

        public MeetupManagerDatabase()
        {
            Connection = new SQLiteConnection(Path.Combine(Root, location));
            //create tables
            Connection.CreateTable<EventRSVP>();
            Connection.CreateTable<NewMember>();
        }

        /// <summary>
        /// Gets all items of type T
        /// </summary>
        /// <typeparam name="T">Type of item to get</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
			
            return (from i in Connection.Table<T>()
                             select i);
			
        }

        /// <summary>
        /// Gets all items of type T
        /// </summary>
        /// <typeparam name="T">Type of item to get</typeparam>
        /// <returns></returns>
        public EventRSVP GetEventRSVP(string eventId, string userId)
        {

            var items = (from i in Connection.Table<EventRSVP>()
                                  where i.UserId == userId && i.EventId == eventId
                                  select i);

            return items.Any() ? items.ElementAt(0) : null;

        }

        public IEnumerable<EventRSVP> GetRSVPsByDate(string groupId)
        {
            return  (from i in Connection.Table<EventRSVP>()
                              where i.GroupId == groupId
                              select i).OrderBy(n => n.EventDate);
        }

        public IEnumerable<NewMember> GetNewMembersByDate(string groupId)
        {
            return  (from i in Connection.Table<NewMember>()
                              where i.GroupId == groupId
                              select i).OrderBy(n => n.EventDate);
        }

        /// <summary>
        /// Gets all items of type T
        /// </summary>
        /// <typeparam name="T">Type of item to get</typeparam>
        /// <returns></returns>
        public IEnumerable<NewMember> GetNewMembers(string eventId)
        {
           
            var items = (from i in Connection.Table<NewMember>()
                                  where i.EventId == eventId
                                  select i);

            return items;
        }

        /// <summary>
        /// Gets a specific item of type T with specified ID
        /// </summary>
        /// <typeparam name="T">Type of item to get</typeparam>
        /// <param name="id">ID to get</param>
        /// <returns>Item type T or null.</returns>
        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
           
            return (from i in Connection.Table<T>()
                                 where i.Id == id
                                 select i).FirstOrDefault();
           
        }

        /// <summary>
        /// Save and update item of type T. If ID is set then will update the item, eelse creates new and returns the id.
        /// </summary>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <param name="item">Item to save or update</param>
        /// <returns>ID of item</returns>
        public int SaveItem<T>(T item) where T : IBusinessEntity
        {
            
            if (item.Id != 0)
            {
                Connection.Update(item);
                return item.Id;
            }
                
            return Connection.Insert(item);

        }

        /// <summary>
        /// Saves a list of items calling SaveItems in 1 transaction
        /// </summary>
        /// <typeparam name="T">Type of entity to save</typeparam>
        /// <param name="items">List of items</param>
        public void SaveItems<T>(IEnumerable<T> items) where T : IBusinessEntity
        {
            
            Connection.BeginTransaction();

            foreach (T item in items)
            {
                SaveItem(item);
            }

            Connection.Commit();

        }

        /// <summary>
        /// Deletes a specific item with id specified
        /// </summary>
        /// <typeparam name="T">Type of item to delete</typeparam>

        /// <param name="item"></param>
        /// <returns></returns>
        public int DeleteItem<T>(T item) where T : IBusinessEntity, new()
        {
           
            return Connection.Delete(item);

        }



        /// <summary>
        /// Deletes a specific item with id specified
        /// </summary>
        /// <typeparam name="T">Type of item to delete</typeparam>
        /// <param name="id">Item to delete</param>
        /// <returns></returns>
        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            
            return this.Connection.Delete<T>(id);

        }

        /// <summary>
        /// Clear an entire table of specific type
        /// </summary>
        /// <typeparam name="T">Type to clear table</typeparam>
        public void ClearTable<T>() where T : IBusinessEntity, new()
        {
            
            Connection.Execute(string.Format("delete from \"{0}\"", typeof(T).Name));

        }
    }
}
