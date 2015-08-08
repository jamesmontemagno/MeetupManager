using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MeetupManager.Portable.Models;
using MeetupManager.Portable.ViewModels;
using MeetupManager.Portable.Helpers;

namespace MeetupManager.Portable.Views
{
    public partial class EventsView : ContentPage
    {
        EventsViewModel viewModel;
        bool allEvents;
        public EventsView(Group @group)
        {
            InitializeComponent();

            BindingContext = viewModel = new EventsViewModel(this, @group);
            allEvents = Settings.ShowAllEvents;
            ToolbarItems.Add(new ToolbarItem
                {
                    StyleId="Statistics",
                    Icon = "ic_action_trending_up.png",
                    Text = "Statistics",
                    Order = ToolbarItemOrder.Primary,
                    Command = viewModel.GoToStatsCommand
                });

            ToolbarItems.Add(new ToolbarItem
                {
                    StyleId="Settings",
                    Icon = "ic_action_settings.png",
                    Text = "Settings",
                    Order = ToolbarItemOrder.Primary,
                    Command = viewModel.GoToAboutCommand
                });

            EventsList.ItemSelected += (sender, e) => 
                {
                    var selected = EventsList.SelectedItem as Event;
                    if(selected == null)
                        return;

                    viewModel.GoToEventCommand.Execute(selected);
                    EventsList.SelectedItem = null;
                };

            EventsList.ItemAppearing += (sender, e) => 
                { 
                    if(viewModel.IsBusy || viewModel.Events.Count == 0) 
                        return; 
                    //hit bottom! 
                    if(((Event)e.Item).Id == viewModel.Events[viewModel.Events.Count - 1].Id) 
                    { 
                        viewModel.LoadMoreCommand.Execute(null); 
                    } 
                }; 
                   
            //ensure after first load that we scroll to the top.
            /*viewModel.FinishedFirstLoad = (index) =>
            {
                    if(viewModel.Events.Count == 0)   
                        return;
                    Device.StartTimer(TimeSpan.FromMilliseconds(100), ()=>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                                EventsList.ScrollTo(viewModel.Events[index], ScrollToPosition.MakeVisible, false));

                            return false;
                        });
            };*/
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           
            if (viewModel.IsBusy)
                return;

            var forceRefresh = allEvents != Settings.ShowAllEvents;
            allEvents = Settings.ShowAllEvents;
            if (viewModel.Events.Count > 0 && !forceRefresh)
                return;

            viewModel.RefreshCommand.Execute(null);
        }
    }
}

