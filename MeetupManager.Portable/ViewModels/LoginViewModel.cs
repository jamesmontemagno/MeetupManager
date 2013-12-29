using System;
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
				ShowViewModel<EventsViewModel> ();
			}
		}


		private IMvxCommand loginCommand;
		public IMvxCommand LoginCommand
		{
			get { return loginCommand ?? (loginCommand = new MvxCommand (ExecuteLoginCommand)); }
		}

		private async Task RenewAccessToken()
		{
			IsBusy = true;
			bool success = await meetupService.RenewAccessToken ();
			IsBusy = false;

			if(success)
				ShowViewModel<EventsViewModel> ();
		}

		private void ExecuteLoginCommand()
		{
			login.LoginAsync (/*(success) => {
				if(success)
					ShowViewModel<EventsViewModel>();
			}*/);
		}
	}
}

