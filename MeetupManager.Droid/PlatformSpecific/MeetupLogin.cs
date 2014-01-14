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
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.Services;
using Xamarin.Auth;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using MeetupManager.Portable.Helpers;

namespace MeetupManager.Droid.PlatformSpecific
{
	public class MeetupLogin : ILogin
	{

		#region ILogin implementation

		OAuth2Authenticator auth = new OAuth2Authenticator (
			clientId: MeetupService.ClientId,
			clientSecret: MeetupService.ClientSecret,
			scope: "",
			authorizeUrl: new Uri ("https://secure.meetup.com/oauth2/authorize"),
			redirectUrl: new Uri ("http://www.refractored.com/login_success.html"),
			accessTokenUrl: new Uri("https://secure.meetup.com/oauth2/access"));

		public void LoginAsync (Action<bool> loginCallback)
		{

			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity> ().Activity;

			auth.AllowCancel = true;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += (s, ee) => {

			    if (ee.IsAuthenticated)
			    {
			        Settings.AccessToken = ee.Account.Properties["access_token"];
			        Settings.RefreshToken = ee.Account.Properties["refresh_token"];

			        long time = 0;
			        long.TryParse(ee.Account.Properties["expires_in"], out time);
					var nextTime = DateTime.UtcNow.AddSeconds(time).Ticks;
					Settings.KeyValidUntil = nextTime;
			    }

			    if (loginCallback != null)
                    loginCallback(ee.IsAuthenticated);
			};
            

			var intent = auth.GetUI (activity);
			activity.StartActivity (intent);
		}


		#endregion


	}
}

