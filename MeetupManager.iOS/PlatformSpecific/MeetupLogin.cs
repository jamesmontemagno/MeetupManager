using System;
using System.Collections.Generic;
using Xamarin.Auth;
using MeetupManager.Portable.Services;
using MonoTouch.UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Views.Presenters;
using MeetupManager.Portable.Interfaces;

namespace MeetupManager.iOS.PlatformSpecific
{
	public class MeetupLogin: ILogin
	{
		#region ILogin implementation

    OAuth2Authenticator auth = new OAuth2Authenticator(MeetupService.ClientId, MeetupService.ClientSecret, string.Empty, new Uri(MeetupService.AuthorizeUrl), new Uri(MeetupService.RedirectUrl), new Uri(MeetupService.AccessTokenUrl));

		UIViewController vc = null;
        public void LoginAsync(Action<bool, Dictionary<string, string>> loginCallback)
		{
			var presenter = Mvx.Resolve<IMvxTouchViewPresenter> ();

			auth.AllowCancel = true;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += (s, ee) => {
				vc.DismissViewController (true, null);
				if (loginCallback != null)
					loginCallback (ee.IsAuthenticated, ee.Account == null ? null : ee.Account.Properties);
			};

			auth.Error += (sender, e) => {
				vc.DismissViewController (true, null);
			};




			vc = auth.GetUI ();
			presenter.PresentModalViewController (vc, true);
		}


		#endregion


	}
}

