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
using MeetupManager.Portable.Interfaces.Database;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;

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
