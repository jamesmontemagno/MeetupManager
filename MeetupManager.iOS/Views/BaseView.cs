using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using GoogleAnalytics.iOS;

namespace MeetupManager.iOS.Views
{
	public class BaseViewController : MvxViewController
	{
		protected string Tag { get; set; }
		public BaseViewController (string xib, NSBundle bundle) : base(xib, bundle)
		{
		}

		public void LogEvent(string category, string action, string label)
		{
			#if !DEBUG
			if(GAI.SharedInstance.DefaultTracker == null)
			  return;
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent(category, action, label, null).Build ());
			#endif
		}

		#if !DEBUG
		public override void ViewDidAppear (bool animated)
		{
		base.ViewDidAppear (animated);
		if(GAI.SharedInstance.DefaultTracker == null)
		  return;
		// This screen name value will remain set on the tracker and sent with
		// hits until it is set to a new value or to null.
		GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, Tag);

		GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
		#endif
	}

	public class BaseTableViewController : MvxTableViewController
	{
		protected string Tag { get; set; }
		public BaseTableViewController(UITableViewStyle style) : base(style)
		{
		}

		public void LogEvent(string category, string action, string label)
		{
			#if !DEBUG
			if(GAI.SharedInstance.DefaultTracker == null)
			  return;
			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateEvent(category, action, label, null).Build ());
			#endif
		}


		#if !DEBUG
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			if(GAI.SharedInstance.DefaultTracker == null)
			  return;
			// This screen name value will remain set on the tracker and sent with
			// hits until it is set to a new value or to null.
			GAI.SharedInstance.DefaultTracker.Set (GAIConstants.ScreenName, Tag);

			GAI.SharedInstance.DefaultTracker.Send (GAIDictionaryBuilder.CreateAppView ().Build ());
		}
		#endif
	}
}

