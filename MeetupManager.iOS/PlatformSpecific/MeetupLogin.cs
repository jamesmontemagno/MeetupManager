using System;
using System.Collections.Generic;
using MeetupManager.iOS.PlatformSpecific;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.Services;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly:Dependency(typeof(MeetupLogin))]
namespace MeetupManager.iOS.PlatformSpecific
{
	public class MeetupLogin: ILogin
	{
		#region ILogin implementation

    
		UIViewController vc = null;
        public void LoginAsync(Action<bool, Dictionary<string, string>> loginCallback)
		{
            var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
			var auth = new OAuth2Authenticator(MeetupService.ClientId, MeetupService.ClientSecret, string.Empty, new Uri(MeetupService.AuthorizeUrl), new Uri(MeetupService.RedirectUrl), new Uri(MeetupService.AccessTokenUrl));

			auth.AllowCancel = true;
            auth.ShowUIErrors = false;
			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += (s, ee) => {
				vc.DismissViewController (true, null);
				if (loginCallback != null)
					loginCallback (ee.IsAuthenticated, ee.Account == null ? null : ee.Account.Properties);
			};

            auth.Error += (sender, e) =>
            {
                //vc.DismissViewController(true, null);
                //if (loginCallback != null)
                //    loginCallback (false, null);
                    
            };

			vc = auth.GetUI ();
            controller.PresentViewControllerAsync (vc, true);
		}


		#endregion


	}
}

