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
using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Helpers;

namespace MeetupManager.Portable.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private ILogin login;
		private IMessageDialog dialog;
		public LoginViewModel (IMeetupService meetupService, ILogin login, IMessageDialog dialog) : base(meetupService) 
		{
			this.login = login;
			this.dialog = dialog;

			ExecuteRefreshLoginCommand ();
		}

        private IMvxCommand refreshLoginCommand;
        public IMvxCommand RefreshLoginCommand
        {
            get { return refreshLoginCommand ?? (refreshLoginCommand = new MvxCommand(ExecuteRefreshLoginCommand)); }
        }

		private void ExecuteRefreshLoginCommand()
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
                ShowViewModel<GroupsViewModel>();
            }
	    }

		private MvxCommand loginCommand;
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
				dialog.SendToast("Please login again to re-validate credentials.");

		}

		private MvxCommand showInfoCommand;
		public IMvxCommand ShowInfoCommand
		{
			get  { return showInfoCommand ?? (showInfoCommand ?? new MvxCommand (ExecuteShowInfoCommand)); }
		}

		private void ExecuteShowInfoCommand()
		{
			dialog.SendMessage ("Created by @JamesMontemagno Copyright 2014 Refractored LLC. Open source and created completely in C# with Xamarin!", "About");
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
                        Settings.UserName = user.Name??string.Empty;
			            
			        }
			        catch (Exception ex)
			        {
			        }

			        IsBusy = false;
					dialog.SendToast("Hello there, " + Settings.UserName);
                    ShowViewModel<GroupsViewModel>();
			    }
			    else
			    {
                    InvokeOnMainThread(() =>
                    {
						dialog.SendToast("Unable to login, please try again.");
                        
                        IsBusy = false;
                    });
			    }
			});
		}

	}
}

