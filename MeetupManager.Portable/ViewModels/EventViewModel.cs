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
using MeetupManager.Portable.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Models;
using MeetupManager.Portable.Models.Database;
using System.Linq;

namespace MeetupManager.Portable.ViewModels
{
	public class EventViewModel : BaseViewModel
	{
		private string eventId;
		private IMessageDialog messageDialog;
		private Random random;
        private IDataService dataService;
		public EventViewModel(IMeetupService meetupService, IMessageDialog messageDialog, IDataService dataService) : base(meetupService)
		{
			members = new ObservableCollection<MemberViewModel> ();
			this.messageDialog = messageDialog;
		    this.dataService = dataService;
			random = new Random ();
		}

		public void Init(string eventId, string eventName)
		{
			this.eventId = eventId;
		    this.EventName = eventName;
			ExecuteRefreshCommand ();
		}

        public string EventName { get; set; }

        private string rsvpCount;
        public string RSVPCount
        {
            get { return rsvpCount; }
            set { rsvpCount = value; RaisePropertyChanged(() => RSVPCount); }
        }
       

		private ObservableCollection<MemberViewModel> members;
		public ObservableCollection<MemberViewModel>  Members
		{ 
			get { return members; }
			set { members = value; RaisePropertyChanged(() => Members); }
		}

		private IMvxCommand refreshCommand;
		public IMvxCommand RefreshCommand
		{
			get { return refreshCommand ?? (refreshCommand = new MvxCommand (async ()=>ExecuteRefreshCommand())); }
		}

		private async Task ExecuteRefreshCommand()
		{
			members.Clear ();
			RaisePropertyChanged (() => Members);
		    CanLoadMore = true;
			await ExecuteLoadMoreCommand ();

		}

		private IMvxCommand loadMoreCommand;
		public IMvxCommand LoadMoreCommand
		{
			get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand (async ()=>ExecuteLoadMoreCommand())); }
		}

		private async Task ExecuteLoadMoreCommand()
		{
		    if (!CanLoadMore)
		        return;

			//Go to database and check this user in.
			IsBusy = true;


			try
			{
			    var addNewMembers = Members.Count == 0;
				var eventResults = await this.meetupService.GetRSVPs(eventId, members.Count);
				foreach(var e in eventResults.RSVPs)
				{
					var member = new MemberViewModel(e.Member, e.MemberPhoto, eventId);

					member.CheckedIn = await Mvx.Resolve<IDataService> ().IsCheckedIn (eventId, member.Member.MemberId.ToString());

					members.Add(member);
				}

                CanLoadMore = eventResults.RSVPs.Count == 100;

			    if (addNewMembers)
			    {
			        var newMembers = await dataService.GetNewMembers(eventId);
			        foreach (var e in newMembers)
			        {
			            var member = new MemberViewModel(new Member {MemberId = -1, Name = e.Name},
			                new MemberPhoto
			                {
			                    HighResLink = null,
			                    PhotoId = -1,
			                    PhotoLink = null,
			                    ThumbLink = null
                            }, eventId);
			            member.NewUserId = e.Id;
			            member.CheckedIn = true;
			            members.Add(member);
			        }
			    }



                RefreshCount();

			}
			catch(Exception ex) {
				Mvx.Resolve<IMvxTrace> ().Trace (MvxTraceLevel.Error, "EventViewModel", ex.ToString ());
			    CanLoadMore = false;
                messageDialog.SendToast("Unable to get RSVPs please refresh or log in again.");
			}
			finally{
				IsBusy = false;
			}
		}

		private MvxCommand<MemberViewModel> checkInCommand;
	    
		public IMvxCommand CheckInCommand
		{
			get { return checkInCommand ?? (checkInCommand = new MvxCommand<MemberViewModel> (async (ev)=>ExecuteCheckInCommand(ev))); }
		}

		private async Task ExecuteCheckInCommand(MemberViewModel member)
		{
		    if (member.Member.MemberId == -1)
		    {
                messageDialog.SendToast("New members can only be removed.");
		        return;
		    }
		    if (member.CheckedIn)
                await dataService.CheckOutMember(eventId, member.Member.MemberId.ToString());
            else
			    await dataService.CheckInMember (new EventRSVP (eventId, member.Member.MemberId.ToString()));
            member.CheckedIn = !member.CheckedIn;

            RefreshCount();
		}

		private IMvxCommand selectWinner;
		public IMvxCommand SelectWinnerCommand
		{
			get { return selectWinner ?? (selectWinner = new MvxCommand (async ()=>ExecuteSelectWinnerCommand())); }
		}

		private async Task ExecuteSelectWinnerCommand()
		{
			var potential = members.Where (m => m.CheckedIn).ToList();
			var count = potential.Count;
			var message = string.Empty;
			if (count == 0) {
				message = "No one has checked in.";
			} else if (count == 1) {
				message = potential[0].Name + " | " + potential[0].Member.MemberId;
			} else
			{
			    var member = potential[random.Next(count)];
				message = member.Name + " | " + member.Member.MemberId;
			}

			messageDialog.SendMessage (message, "Winner!!!");
		}


        private MvxCommand<string> saveUserCommand;
        public IMvxCommand SaveUserCommand
        {
            get { return saveUserCommand ?? (saveUserCommand = new MvxCommand<string>(async (name) => ExecuteSaveUserCommand(name))); }
        }

        private async Task ExecuteSaveUserCommand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                messageDialog.SendToast("Please enter a valid name to check in.");
            }
            else
            {
                var newMember = new NewMember(eventId, name);
                await dataService.AddNewMember(newMember);
                var member = new MemberViewModel(new Member { MemberId = -1, Name = name },
                       new MemberPhoto
                       {
                           HighResLink = null,
                           PhotoId = -1,
                           PhotoLink = null,
                           ThumbLink = null
                       }, eventId);

                member.CheckedIn = true;
                member.NewUserId = newMember.Id;
                Members.Add(member);
                RaisePropertyChanged(() => Members);
                RefreshCount();
                messageDialog.SendMessage(name + " you are all set!");
            }
        }

        private MvxCommand<MemberViewModel> deleteUserCommand;

        public IMvxCommand DeleteUserCommand
        {
            get { return deleteUserCommand ?? (deleteUserCommand = new MvxCommand<MemberViewModel>(async (ev) => ExecutedeleteUserCommand(ev))); }
        }

        private async Task ExecutedeleteUserCommand(MemberViewModel member)
        {
            if (member.NewUserId == 0)
                return;
            var id = member.NewUserId;
            var index = Members.FirstOrDefault(m => m.NewUserId == id);
            if (index == null)
                return;
            messageDialog.SendConfirmation("Are you sure you want to remove: " + member.Name, "Remove?",
                (confirmation) =>
                {
                    if (!confirmation)
                        return;
                    Members.Remove(member);
                    dataService.RemoveNewMember(id);
                    RefreshCount();
                });
            
        }

	    private void RefreshCount()
	    {
            RSVPCount = members.Where(m => m.CheckedIn).ToList().Count + "/" + members.Count;
	    }
	}
}

