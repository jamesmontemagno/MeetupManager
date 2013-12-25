using System;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
			//Go to database and check this user in.

		}

		private IMvxCommand loadMoreCommand;
		public IMvxCommand LoadMoreCommand
		{
			get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand (async ()=>ExecuteLoadMoreCommand())); }
		}

		private async Task ExecuteLoadMoreCommand()
		{
			//Go to database and check this user in.

		}
	}
}

