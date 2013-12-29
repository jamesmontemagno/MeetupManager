using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MeetupManager.Portable.Interfaces;
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
			clientId: "kgqtisiigj7mpbpbfs1ei7s2h0",
			clientSecret: "g4k3oiourvnos0nf9varqt5eaf",
			scope: "",
			authorizeUrl: new Uri ("https://secure.meetup.com/oauth2/authorize"),
			redirectUrl: new Uri ("http://www.refractored.com/login_success.html"),
			accessTokenUrl: new Uri("https://secure.meetup.com/oauth2/access"));

		public void LoginAsync ()
		{
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity> ().Activity;

			auth.AllowCancel = true;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += (s, ee) => {


				//callback(ee.IsAuthenticated);
				Settings.AccessToken = ee.Account.Properties["access_token"];
				Settings.RefreshToken = ee.Account.Properties["refresh_token"];

				long time = 0;
				long.TryParse(ee.Account.Properties["expires_in"], out time);
				Settings.KeyValidUntil = DateTime.UtcNow.Ticks + time;
			};

			var intent = auth.GetUI (activity);
			activity.StartActivity (intent);
		}

		public string AccountId {
			get {
				return string.Empty;
			}
		}

		#endregion


	}
}

