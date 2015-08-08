using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Portable.Views
{
    public partial class LoginView : ContentPage
    {
        LoginViewModel viewModel;
        public LoginView()
        {
            InitializeComponent();

            viewModel = new LoginViewModel(this);

            ToolbarItems.Add(new ToolbarItem
                {
                    StyleId="Settings",
                    Icon = "ic_action_settings.png",
                    Text = "Settings",
                    Order = ToolbarItemOrder.Primary,
                    Command = viewModel.GoToAboutCommand
                });

            ToolbarItems.Add(new ToolbarItem
                {
                    Icon = "ic_action_navigation_arrow_forward.png",
                    Text = "Already Logged In",
                    Order = ToolbarItemOrder.Primary,
                    Command = viewModel.RefreshLoginCommand
                });

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext == null)
            {
                BindingContext = viewModel;
                viewModel.RefreshLoginCommand.Execute(null);
            }
        }
    }
}

