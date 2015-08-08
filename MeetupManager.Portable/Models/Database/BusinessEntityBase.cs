
using MeetupManager.Portable.Interfaces.Database;
using SQLite;

namespace MeetupManager.Portable.Models.Database
{
    /// <summary>
    /// All database items should derive from this as it implemetnes the IBusinessEntity
    /// </summary>
    public class BusinessEntityBase : IBusinessEntity
    {
        public BusinessEntityBase()
        {
        }

        /// <summary>
        /// Gets or sets the Database ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
