using System;
using Xamarin.Auth;
using MeetupManager.Portable.Services;
using MeetupManager.Portable.Helpers;
using MonoTouch.UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MeetupManager.Portable.Interfaces;

namespace MeetupManager.iOS.PlatformSpecific
{
	public class MeetupLogin: ILogin
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

			var presenter = Mvx.Resolve<IMvxTouchViewPresenter> ();
			UIViewController vc = null;
			auth.AllowCancel = true;

			// If authorization succeeds or is canceled, .Completed will be fired.
			var vc1 = vc;
			auth.Completed += (s, ee) => {

				vc1.DismissViewController(true, null);
				if (ee.IsAuthenticated)
				{
					Settings.AccessToken = ee.Account.Properties["access_token"];
					Settings.RefreshToken = ee.Account.Properties["refresh_token"];

					long time = 0;
					long.TryParse(ee.Account.Properties["expires_in"], out time);
					Settings.KeyValidUntil = DateTime.UtcNow.Ticks + time;
				}

				if (loginCallback != null)
					loginCallback(ee.IsAuthenticated);
			};


			vc = auth.GetUI ();
			presenter.PresentModalViewController (vc, true);


		}


		#endregion


	}
}

