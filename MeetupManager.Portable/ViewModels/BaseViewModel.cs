using System;
using MeetupManager.Portable.Interfaces;
using Cirrious.MvvmCross.ViewModels;

namespace MeetupManager.Portable.ViewModels
{
	public class BaseViewModel : MvxViewModel
	{
		internal readonly IMeetupService meetupService;
		public BaseViewModel (IMeetupService meetupService)
		{
			this.meetupService = meetupService;
		}

		private bool isBusy = false;
		public bool IsBusy
		{ 
			get { return isBusy; }
			set { isBusy = value; RaisePropertyChanged(() => IsBusy); }
		}
	}
}

