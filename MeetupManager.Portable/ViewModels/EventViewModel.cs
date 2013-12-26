using System;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Models.Database;

namespace MeetupManager.Portable.ViewModels
{
	public class EventViewModel : BaseViewModel
	{
		private string eventId;
		public EventViewModel(IMeetupService meetupService) : base(meetupService)
		{
			members = new ObservableCollection<MemberViewModel> ();
		}

		public void Init(string eventId)
		{
			this.eventId = eventId;
			ExecuteRefreshCommand ();
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
		public IMvxCommand CheckInCommand
		{
			get { return checkInCommand ?? (checkInCommand = new MvxCommand<MemberViewModel> (async (ev)=>ExecuteCheckInCommand(ev))); }
		}

		private async Task ExecuteCheckInCommand(MemberViewModel member)
		{

			await Mvx.Resolve<IDataService> ().CheckInMember (new EventRSVP (eventId, member.Member.MemberId.ToString()));
			member.CheckedIn = true;
		

		}
	}
}

