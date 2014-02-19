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
using MeetupManager.Portable.Models.Database;
using System.Threading.Tasks;

namespace MeetupManager.Portable.Interfaces.Database
{
    /// <summary>
    /// Data service intrface for all interactions with the database.
    /// </summary>
    public interface IDataService
    {
				Task CheckInMember (EventRSVP rsvp);
        Task CheckOutMember(string eventId, string userId);
        Task<bool> IsCheckedIn(string eventId, string userId, string eventName, string groupId, string groupName, long eventDate);

        Task AddNewMember(NewMember member);
        Task<IEnumerable<NewMember>> GetNewMembers(string eventId);
        Task RemoveNewMember(int id);
		Task<IEnumerable<NewMember>> GetNewMembersForGroup (string groupId);
		Task<IEnumerable<EventRSVP>> GetRSVPsForGroup (string groupId);
    }
}
