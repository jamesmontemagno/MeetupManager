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
using MeetupManager.Portable.Models.Database;
using System.Linq;

namespace MeetupManager.Portable.ViewModels
{
	public class EventViewModel : BaseViewModel
	{
		private string eventId;
		private IMessageDialog messageDialog;
		private Random random;
		public EventViewModel(IMeetupService meetupService, IMessageDialog messageDialog) : base(meetupService)
		{
			members = new ObservableCollection<MemberViewModel> ();
			this.messageDialog = messageDialog;
			random = new Random ();
		}

		public void Init(string eventId, string eventName)
		{
			this.eventId = eventId;
		    this.EventName = eventName;
			ExecuteRefreshCommand ();
		}

        public string EventName { get; set; }

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
			await ExecuteLoadMoreCommand ();

		}

		private IMvxCommand loadMoreCommand;
		public IMvxCommand LoadMoreCommand
		{
			get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand (async ()=>ExecuteLoadMoreCommand())); }
		}

		private async Task ExecuteLoadMoreCommand()
		{
			//Go to database and check this user in.
			IsBusy = true;


			try{
				var eventResults = await this.meetupService.GetRSVPs(eventId, members.Count);
				foreach(var e in eventResults.RSVPs)
				{
					var member = new MemberViewModel(e.Member, e.MemberPhoto, eventId);
					member.CheckedIn = await Mvx.Resolve<IDataService> ().IsCheckedIn (eventId, member.Member.MemberId.ToString());

					members.Add(member);
				}
			}
			catch(Exception ex) {
				Mvx.Resolve<IMvxTrace> ().Trace (MvxTraceLevel.Error, "EventViewModel", ex.ToString ());
			}
			finally{
				IsBusy = false;
			}
		}

		private MvxCommand<MemberViewModel> checkInCommand;
	    private IDataService dataService;
		public IMvxCommand CheckInCommand
		{
			get { return checkInCommand ?? (checkInCommand = new MvxCommand<MemberViewModel> (async (ev)=>ExecuteCheckInCommand(ev))); }
		}

		private async Task ExecuteCheckInCommand(MemberViewModel member)
		{
		    if (dataService == null)
		        dataService = Mvx.Resolve<IDataService>();
            if (member.CheckedIn)
                await dataService.CheckOutMember(eventId, member.Member.MemberId.ToString());
            else
			    await dataService.CheckInMember (new EventRSVP (eventId, member.Member.MemberId.ToString()));
            member.CheckedIn = !member.CheckedIn;
		

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
			    var member = potential[random.Next(0, count - 1)];
				message = member.Name + " | " + member.Member.MemberId;
			}

			messageDialog.SendMessage (message, "Winner!!!");
		}
	}
}

