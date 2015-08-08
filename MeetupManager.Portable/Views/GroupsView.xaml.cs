using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using MeetupManager.Portable.ViewModels;
using MeetupManager.Portable.Models;
using MeetupManager.Portable.Helpers;

namespace MeetupManager.Portable.Views
{
    public partial class GroupsView : ContentPage
    {
        GroupsViewModel viewModel;
        bool managerMode;
        public GroupsView()
        {
            InitializeComponent();
            BindingContext = viewModel = new GroupsViewModel(this);
            managerMode = Settings.OrganizerMode;

            ToolbarItems.Add(new ToolbarItem
                {
                    StyleId="Settings",
                    Icon = "ic_action_settings.png",
                    Text = "Settings",
                    Order = ToolbarItemOrder.Primary,
                    Command = viewModel.GoToAboutCommand
                });
            
            GroupsList.ItemSelected += (sender, e) => 
                {
                    var selected = GroupsList.SelectedItem as Group;
                    if(selected == null)
                        return;

                    viewModel.GoToGroupCommand.Execute(selected);
                    GroupsList.SelectedItem = null;
                };

            GroupsList.ItemAppearing += (sender, e) => 
                { 
                    if(viewModel.IsBusy || viewModel.Groups.Count == 0) 
                        return; 
                    //hit bottom! 
                    if(((Group)e.Item).Id == viewModel.Groups[viewModel.Groups.Count - 1].Id) 
                    { 
                        viewModel.LoadMoreCommand.Execute(null); 
                    } 
                }; 

            //ensure after first load that we scroll to the top.
            /*viewModel.FinishedFirstLoad = (index) =>
                {
                    if(viewModel.Groups.Count == 0)   
                        return;

                    Device.StartTimer(TimeSpan.FromMilliseconds(100), ()=>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                                GroupsList.ScrollTo(viewModel.Groups[index], ScrollToPosition.MakeVisible, false));

                            return false;
                        });
                };*/
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.IsBusy)
                return;

            var forceRefresh = managerMode != Settings.OrganizerMode;
            managerMode = Settings.OrganizerMode;
            if(!forceRefresh && viewModel.Groups.Count > 0)
                return;

            await viewModel.ExecuteRefreshCommand();

        }
    }
}

