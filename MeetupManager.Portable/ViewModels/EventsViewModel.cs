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
	public class EventsViewModel 
		: BaseViewModel
    {
		public EventsViewModel(IMeetupService meetupService) : base(meetupService)
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

		public async Task ExecuteRefreshCommand()
		{
			events.Clear ();
			RaisePropertyChanged (() => Events);
			await ExecuteLoadMoreCommand ();
		}

		private MvxCommand loadMoreCommand;
		public IMvxCommand LoadMoreCommand
		{
			get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand (async ()=>ExecuteLoadMoreCommand())); }
		}

		private async Task ExecuteLoadMoreCommand()
		{
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

		private MvxCommand<Event> goToEventCommand;
		public IMvxCommand GoToEventCommand
		{
			get { return goToEventCommand ?? (goToEventCommand = new MvxCommand<Event> (async (ev)=>ExecuteGoToEventCommand(ev))); }
		}

		private async Task ExecuteGoToEventCommand(Event e)
		{
			ShowViewModel<EventViewModel>(new { Id = e.Id});
		}
    }
}
