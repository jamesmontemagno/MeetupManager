using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MeetupManager.Portable.Models;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Portable.Views
{
    public partial class EventView : ContentPage
    {
        EventViewModel viewModel;
        public EventView(Event e, string gId, string gName)
        {
            InitializeComponent();

            BindingContext = viewModel = new EventViewModel(this, e, gId, gName);

            ToolbarItems.Add(new ToolbarItem
                {
                    StyleId="AddNewMember",
                    Text = "Add New Member",
                    Order = ToolbarItemOrder.Primary,
                    Icon = "ic_action_social_person_add.png",
                    Command = viewModel.AddNewUserCommand
                });
            ToolbarItems.Add(new ToolbarItem
                {
                    StyleId="PickWinner",
                    Text = "Pick Winner",
                    Icon = "ic_action_winner.png",
                    Order = ToolbarItemOrder.Primary,
                    Command = viewModel.SelectWinnerCommand
                });

            MembersList.ItemSelected += (sender, ee) => 
                {
                    if (MembersList.SelectedItem == null)
                        return;

                    viewModel.CheckInCommand.Execute(ee.SelectedItem);
                    MembersList.SelectedItem = null;
                };

            MembersList.ItemAppearing += (sender, ee) => 
                { 
                    if(viewModel.IsBusy || viewModel.Members.Count == 0) 
                        return; 
                    //hit bottom! 
                    if(((MemberViewModel)ee.Item).Name == viewModel.Members[viewModel.Members.Count - 1].Member.Name) 
                    { 
                        viewModel.LoadMoreCommand.Execute(null); 
                    } 
                }; 

            //ensure after first load that we scroll to the top.
            /*viewModel.FinishedFirstLoad = (index) =>
                {
                    if(viewModel.Members.Count == 0)   
                        return;
                    Device.StartTimer(TimeSpan.FromMilliseconds(100), ()=>
                        {
                            Device.BeginInvokeOnMainThread(() =>
                                MembersList.ScrollTo(viewModel.Members[index], ScrollToPosition.MakeVisible, false));

                            return false;
                        });
                };*/
            
        }

        public void OnRemove (object sender, EventArgs e) {
            var mi = ((MenuItem)sender);
            viewModel.DeleteUserCommand.Execute (mi.CommandParameter);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.IsBusy || viewModel.Members.Count > 0)
                return;

            viewModel.RefreshCommand.Execute(null);
        }
    }
}

