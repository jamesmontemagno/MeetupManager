using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Interfaces;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MeetupManager.Portable.Models;
using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;

namespace MeetupManager.Portable.ViewModels
{
    public class FirstViewModel 
		: BaseViewModel
    {
		public FirstViewModel(IMeetupService meetupService) : base(meetupService)
		{
			events = new ObservableCollection<Event> ();
		}

		private ObservableCollection<Event> events;
		public ObservableCollection<Event> Events
		{
			get { return events; }
			set {
				events = value;
				RaisePropertyChanged (() => Events);
			}
		}

		private IMvxCommand refreshCommand;
		public IMvxCommand RefreshCommand
		{
			get { return refreshCommand ?? (refreshCommand = new MvxCommand (async ()=>  ExecuteRefreshCommand())); }
		}

		private async Task ExecuteRefreshCommand()
		{
			events.Clear ();
			RaisePropertyChanged (() => Events);
			IsBusy = true;


			try{
				var eventResults = await this.meetupService.GetEvents(events.Count);
				foreach(var e in eventResults.Events)
				{
					events.Add(e);
				}
			}
			catch(Exception ex) {
				Mvx.Resolve<IMvxTrace> ().Trace (MvxTraceLevel.Error, "FirstViewModel", ex.ToString ());
			}
			finally{
				IsBusy = false;
			}
		}

		private IMvxCommand loadMoreCommand;
		public IMvxCommand LoadMoreCommand
		{
			get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand (async ()=>ExecuteLoadMoreCommand())); }
		}

		private async Task ExecuteLoadMoreCommand()
		{

		}
    }
}
