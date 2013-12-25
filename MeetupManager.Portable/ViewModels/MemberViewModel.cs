using System;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Models;
using System.Threading.Tasks;

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
		private string eventId;
		public MemberViewModel(Member member, MemberPhoto photo, string eventId)
		{
			this.Member = member;
			this.eventId = eventId;
			this.Photo = photo;
			//go to database and check if user is checked in based on event name and id.
		}

		private IMvxCommand checkInCommand;
		public IMvxCommand CheckInCommand
		{
			get { return checkInCommand ?? (checkInCommand = new MvxCommand (async ()=>ExecuteCheckInCommand())); }
		}

		private async Task ExecuteCheckInCommand()
		{
			//Go to database and check this user in.

		}
	}
}

