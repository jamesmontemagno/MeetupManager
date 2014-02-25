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
	  private string groupId;

		public EventsViewModel(IMeetupService meetupService) : base(meetupService)
		{
			events = new ObservableCollection<Event> ();
		}


    public void Init(string id, string groupName)
    {
        this.groupId = id;
        this.GroupName = groupName;
        ExecuteRefreshCommand();
    }

    public string GroupName { get; set; }
    public string GroupId { get { return groupId; } }

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
			if (IsBusy)
				return;

			events.Clear ();
			RaisePropertyChanged (() => Events);
		    CanLoadMore = true;
			await ExecuteLoadMoreCommand ();
		}

        protected override async Task ExecuteLoadMoreCommand()
		{
			if (!CanLoadMore || IsBusy)
		        return;

			IsBusy = true;

            
			try
			{
				var eventResults = await this.meetupService.GetEvents(groupId, events.Count);
				foreach(var e in eventResults.Events)
				{
					events.Add(e);
				}

                CanLoadMore = eventResults.Events.Count == 100;

                if (Events.Count == 0)
                    Mvx.Resolve<IMessageDialog>().SendToast("There are no events for this group.");
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
			get { return goToEventCommand ?? (goToEventCommand = new MvxCommand<Event> (ExecuteGoToEventCommand)); }
		}

		private void ExecuteGoToEventCommand(Event e)
		{
			ShowViewModel<EventViewModel>(new { eId = e.Id, eName = e.Name, gId = groupId, gName = GroupName, eDate = e.Time});
		}

		private IMvxCommand goToStatsCommand;

		public IMvxCommand GoToStatsCommand
		{
			get { return goToStatsCommand ?? (goToStatsCommand = new MvxCommand(ExecuteGoToStatsCommand)); }
		}

		public void ExecuteGoToStatsCommand()
		{
			ShowViewModel<StatisticsViewModel> (new {gId = this.groupId, gName = this.GroupName});
		}
    }
}
