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
using System.Collections.Generic;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.Services;
using Xamarin.Auth;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;

namespace MeetupManager.Droid.PlatformSpecific
{
	public class MeetupLogin : ILogin
	{
		#region ILogin implementation

		OAuth2Authenticator auth = new OAuth2Authenticator (MeetupService.ClientId, MeetupService.ClientSecret, string.Empty,  new Uri (MeetupService.AuthorizeUrl), new Uri (MeetupService.RedirectUrl), new Uri(MeetupService.AccessTokenUrl));

		public void LoginAsync (Action<bool, Dictionary<string, string>> loginCallback)
		{
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity> ().Activity;

			auth.AllowCancel = true;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += (s, ee) => {
			    if (loginCallback != null)
                    loginCallback(ee.IsAuthenticated, ee.Account.Properties);
			};
            

			var intent = auth.GetUI (activity);
			activity.StartActivity (intent);
		}


		#endregion


	}
}

