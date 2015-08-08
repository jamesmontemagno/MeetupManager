using System;

using Xamarin.Forms;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Portable.Views
{
    public class LoginPage : ContentPage
    {
        public LoginViewModel ViewModel {get;set;}
        public LoginPage(LoginViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}


