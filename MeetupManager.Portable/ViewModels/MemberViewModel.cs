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
using System;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Models;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Models.Database;

namespace MeetupManager.Portable.ViewModels
{
    
	public class MemberViewModel : MvxViewModel
	{
        public static string DefaultIcon = @"http://refractored.com/default.png";
		public Member Member {get;set;}
		private bool checkedIn;
		public bool CheckedIn 
		{
			get{ return checkedIn; }
			set
			{
			    if (checkedIn == value)
			        return;

				checkedIn = value;
				RaisePropertyChanged (() => CheckedIn);
			}
		}

	    public bool CanDelete
	    {
	        get { return NewUserId != 0; }
	    }

        public int NewUserId { get; set; }
		public MemberPhoto Photo{get;set;}
		private readonly string eventId;
		public MemberViewModel(Member member, MemberPhoto photo, string eventId)
		{
			this.Member = member;
			this.eventId = eventId;
			this.Photo = photo ?? new MemberPhoto {
                HighResLink = DefaultIcon,
				PhotoId = 0,
                ThumbLink = DefaultIcon,
                PhotoLink = DefaultIcon
			};

		    if (string.IsNullOrWhiteSpace(this.Photo.HighResLink))
		        this.Photo.HighResLink = DefaultIcon;


            if (string.IsNullOrWhiteSpace(this.Photo.ThumbLink))
                this.Photo.ThumbLink = DefaultIcon;

            if (string.IsNullOrWhiteSpace(this.Photo.PhotoLink))
                this.Photo.PhotoLink = DefaultIcon;
		}

		public string Name { get { return this.Member.Name; } }

		private IMvxCommand checkInCommand;
		public IMvxCommand CheckInCommand
		{
			get { return checkInCommand ?? (checkInCommand = new MvxCommand (async ()=>ExecuteCheckInCommand())); }
		}

		private async Task ExecuteCheckInCommand()
		{

			await Mvx.Resolve<IDataService> ().CheckInMember (new EventRSVP (eventId, this.Member.MemberId.ToString()));

			CheckedIn = true;

		}
	}
}

