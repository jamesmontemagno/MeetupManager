using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using GoogleAnalytics.iOS;

namespace MeetupManager.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : MvxApplicationDelegate
	{
		UIWindow _window;

		public static readonly string TrackingId = "UA-11557716-27";

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			UINavigationBar.Appearance.TintColor = UIColor.Red;
			UINavigationBar.Appearance.BarTintColor = UIColor.White;

			_window.TintColor = UIColor.Red;
			var setup = new Setup(this, _window);
			setup.Initialize();

			#if !DEBUG
			// Optional: set Google Analytics dispatch interval to e.g. 30 seconds.
			GAI.SharedInstance.DispatchInterval = 30;

			// Optional: automatically send uncaught exceptions to Google Analytics.
			GAI.SharedInstance.TrackUncaughtExceptions = true;

			// Initialize tracker.
			GAI.SharedInstance.GetTracker (TrackingId);
			#endif

			var startup = Mvx.Resolve<IMvxAppStart>();
			startup.Start();

			_window.MakeKeyAndVisible ();

			return true;
		}
	}
}

