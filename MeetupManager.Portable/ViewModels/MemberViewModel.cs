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
		public Member Member {get;set;}
		private bool checkedIn;
		public bool CheckedIn 
		{
			get{ return checkedIn; }
			set{
				checkedIn = value;
				RaisePropertyChanged (() => CheckedIn);
			}
		}
		public MemberPhoto Photo{get;set;}
		private readonly string eventId;
		public MemberViewModel(Member member, MemberPhoto photo, string eventId)
		{
			this.Member = member;
			this.eventId = eventId;
			this.Photo = photo;
		}

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

