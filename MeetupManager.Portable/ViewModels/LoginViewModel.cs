
using System;
using System.Windows.Input;
using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.Interfaces;
using Xamarin.Forms;
using MeetupManager.Portable.Views;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetupManager.Portable.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        readonly ILogin login;

        public LoginViewModel(Page page) : base(page)
        {
            login = DependencyService.Get<ILogin>();
        }

        Command refreshLoginCommand;

        public ICommand RefreshLoginCommand
        {
            get { return refreshLoginCommand ?? (refreshLoginCommand = new Command(ExecuteRefreshLoginCommand)); }
        }

        async void ExecuteRefreshLoginCommand()
        {
            if (IsBusy)
                return;

            if (DateTime.UtcNow.Ticks < Settings.KeyValidUntil)
            {
                RenewAccessToken();
            }
            else if (!string.IsNullOrWhiteSpace(Settings.AccessToken) &&
                     !string.IsNullOrWhiteSpace(Settings.RefreshToken))
            {
                
                await page.Navigation.PushAsync(new GroupsView());
            }
        }



        async void RenewAccessToken()
        {
            IsBusy = true;
            bool success = await meetupService.RenewAccessToken();
            IsBusy = false;

            if (success)
            {
                await page.Navigation.PushAsync(new GroupsView());
            }
            else
            {
                messageDialog.SendToast("Please login again to re-validate credentials.");
            }
        }

       
       
        Command loginCommand;

        public ICommand LoginCommand
        {
            get { return loginCommand ?? (loginCommand = new Command(async ()=> await ExecuteLoginCommand())); }
        }

        public async Task FinishLogin(bool success, Dictionary<string, string> properties)
        {
            if (success)
            {
                Settings.AccessToken = properties["access_token"];
                Settings.RefreshToken = properties["refresh_token"];

                long time;
                long.TryParse(properties["expires_in"], out time);
                var nextTime = DateTime.UtcNow.AddSeconds(time).Ticks;
                Settings.KeyValidUntil = nextTime;

                IsBusy = true;
                try
                {
                    var user = await meetupService.GetCurrentMember();
                    Settings.UserId = user.Id.ToString();
                    Settings.UserName = user.Name ?? string.Empty;

                }
                catch (Exception ex)
                {
                    if (Settings.Insights)
                        Xamarin.Insights.Report(ex);
                }

                IsBusy = false;
                messageDialog.SendToast("Hello there, " + Settings.UserName);
                //TODO: Navigate    
                await page.Navigation.PushAsync(new GroupsView());

            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                    {
                        messageDialog.SendMessage("Please try again using your meetup credentials.", "Login Failure");

                        IsBusy = false;
                    });
            }
        }

        async Task ExecuteLoginCommand()
        {
            if (Device.OS == TargetPlatform.Android)
            {
                await page.Navigation.PushAsync(new LoginPage(this));
            }
            /*else if (Device.OS == TargetPlatform.iOS)
            {    
                page.Navigation.PushModalAsync(new LoginPage(this));
            }*/
            else
            {
                login.LoginAsync(async (success, properties) =>
                await FinishLogin(success, properties));
            }
        }

    }
}

