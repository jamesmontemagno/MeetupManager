using System;
using Cirrious.MvvmCross.ViewModels;

namespace MeetupManager.Portable.ViewModels
{
	public class EventViewModel : MvxViewModel
	{
		private string _hello = "Hello MvvmCross";
		public string Hello
		{ 
			get { return _hello; }
			set { _hello = value; RaisePropertyChanged(() => Hello); }
		}
	}
}

