
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.Models;
using Xamarin.Forms;
using MeetupManager.Portable.Views;

namespace MeetupManager.Portable.ViewModels
{
    public class EventsViewModel 
		: BaseViewModel
    {
        
        string groupId;
        public EventsViewModel(Page page, Group @group) : base(page)
        {
            groupId = group.Id.ToString();
            GroupName = group.Name;
            Events = new ObservableCollection<Event>();
            EventsGrouped = new ObservableCollection<Grouping<string, Event>>();
        }

        public string GroupName { get; set; }

        public string GroupId { get { return groupId; } }

        public ObservableCollection<Grouping<string, Event>> EventsGrouped { get; set; }

        public ObservableCollection<Event> Events {get;set;}

        Command refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        public async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            Events.Clear();
            OnPropertyChanged("IsBusy");
            CanLoadMore = true;
            await ExecuteLoadMoreCommand();
        }

        protected override async Task ExecuteLoadMoreCommand()
        {
            if (!CanLoadMore || IsBusy)
                return;

            IsBusy = true;
            var index = Events.Count == 0 ? 0 : Events.Count - 1;
            try
            {
                var eventResults = await meetupService.GetEvents(groupId, Events.Count);
                foreach (var e in eventResults.Events)
                {
                    Events.Add(e);
                }

                CanLoadMore = eventResults.Events.Count == 100;

                if (Events.Count == 0)
                    messageDialog.SendToast("There are no events for this group.");

                Sort();
            }
            catch (Exception ex)
            {
                if (Settings.Insights)
                    Xamarin.Insights.Report(ex);
            }
            finally
            {
               
                FinishedFirstLoad?.Invoke(index);
                IsBusy = false;
            }
        }

        private void Sort()
        {

            EventsGrouped.Clear();
            var sorted = from e in Events
                orderby e.Time descending
            group e by e.Year into eGroup
                select new Grouping<string, Event>(eGroup.Key, eGroup);

            foreach(var sort in sorted)
                EventsGrouped.Add(sort);
        }

        Command<Event> goToEventCommand;

        public ICommand GoToEventCommand
        {
            get { return goToEventCommand ?? (goToEventCommand = new Command<Event>(ExecuteGoToEventCommand)); }
        }

        void ExecuteGoToEventCommand(Event e)
        {
            if (IsBusy)
                return;
            
            page.Navigation.PushAsync(new EventView(e, GroupId, GroupName));
        }

        Command goToStatsCommand;

        public Command GoToStatsCommand
        {
            get { return goToStatsCommand ?? (goToStatsCommand = new Command(ExecuteGoToStatsCommand)); }
        }

        public void ExecuteGoToStatsCommand()
        {
            if (IsBusy)
                return;
            
            page.Navigation.PushAsync(new StatisticsView(GroupId, GroupName));
        }
    }
}
