using System;
using System.Collections.Generic;
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


		UIViewController vc = null;
        public void LoginAsync(Action<bool, Dictionary<string, string>> loginCallback)
		{
			OAuth2Authenticator auth = new OAuth2Authenticator (
				clientId: MeetupService.ClientId,
				clientSecret: MeetupService.ClientSecret,
				scope: "",
				authorizeUrl: new Uri ("https://secure.meetup.com/oauth2/authorize"),
				redirectUrl: new Uri ("http://www.refractored.com/login_success.html"),
				accessTokenUrl: new Uri("https://secure.meetup.com/oauth2/access"));
			var presenter = Mvx.Resolve<IMvxTouchViewPresenter> ();

			auth.AllowCancel = true;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += (s, ee) => {

				vc.DismissViewController (true, null);
				if (loginCallback != null)
					loginCallback (ee.IsAuthenticated, ee.Account.Properties);
			};

			auth.BrowsingCompleted += (object sender, EventArgs e) => 
			{
				var test = true;
			};

			auth.Error += (object sender, AuthenticatorErrorEventArgs e) => {
				var test2 = true;
			};


			vc = auth.GetUI ();
			presenter.PresentModalViewController (vc, true);


		}


		#endregion


	}
}

