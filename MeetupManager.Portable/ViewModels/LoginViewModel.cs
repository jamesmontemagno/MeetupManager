using System;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Helpers;
using System.Threading.Tasks;

namespace MeetupManager.Portable.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private ILogin login;
		public LoginViewModel (IMeetupService meetupService, ILogin login) : base(meetupService) 
		{
			this.login = login;

			if (DateTime.UtcNow.Ticks < Settings.KeyValidUntil) {
				RenewAccessToken ();
			}
			else if (!string.IsNullOrWhiteSpace (Settings.AccessToken) &&
				!string.IsNullOrWhiteSpace(Settings.RefreshToken)) {
                    ShowViewModel<GroupsViewModel>();
			}
		}


	    private IMvxCommand loginCommand;
		public IMvxCommand LoginCommand
		{
			get { return loginCommand ?? (loginCommand = new MvxCommand (ExecuteLoginCommand)); }
		}

		private async void RenewAccessToken()
		{
			IsBusy = true;
			bool success = await meetupService.RenewAccessToken ();
			IsBusy = false;

			if(success)
                ShowViewModel<GroupsViewModel>();
            else
                Mvx.Resolve<IMessageDialog>().SendToast("Please login againt to re-validate credentials.");

		}

		private void ExecuteLoginCommand()
		{
			login.LoginAsync (async (success) => {
			    if (success)
			    {
			        IsBusy = true;
			        try
			        {
                        var user = await meetupService.GetCurrentMember();
                        Settings.UserId = user.Id.ToString();
                        Settings.UserName = user.Name;
                        Mvx.Resolve<IMessageDialog>().SendToast("Hello there, " + user.Name);
			        }
			        catch (Exception ex)
			        {
			        }
			       
			        IsBusy = false;
			        ShowViewModel<GroupsViewModel>();
			    }
			});
		}
	}
}

