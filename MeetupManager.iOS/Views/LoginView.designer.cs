// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace MeetupManager.iOS.Views
{
	partial class LoginView
	{
		[Outlet]
		MonoTouch.UIKit.UIActivityIndicatorView ActivityLoggingIn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton ButtonLogin { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LabelLoggingIn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LabelLoginInfo { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LabelLoginInfo != null) {
				LabelLoginInfo.Dispose ();
				LabelLoginInfo = null;
			}

			if (ActivityLoggingIn != null) {
				ActivityLoggingIn.Dispose ();
				ActivityLoggingIn = null;
			}

			if (ButtonLogin != null) {
				ButtonLogin.Dispose ();
				ButtonLogin = null;
			}

			if (LabelLoggingIn != null) {
				LabelLoggingIn.Dispose ();
				LabelLoggingIn = null;
			}
		}
	}
}
