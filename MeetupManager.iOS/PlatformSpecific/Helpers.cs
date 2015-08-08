using System;
using UIKit;
using Foundation;

namespace MeetupManager.iOS.PlatformSpecific
{
	public static class Helpers
	{
		public static NSObject Invoker;
		/// <summary>
		/// Ensures the invoked on main thread.
		/// </summary>
		/// <param name="action">Action to run on main thread.</param>
		public static void EnsureInvokedOnMainThread (Action action)
		{
			if ( NSThread.Current.IsMainThread) {
				action ();
				return;
			}
			if (Invoker == null)
				Invoker = new NSObject ();

            Invoker.BeginInvokeOnMainThread(action);
		}

		public static bool IsPhone
		{
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public static bool IsiOS7
		{
			get
			{
				return UIDevice.CurrentDevice.CheckSystemVersion (7, 0);
			}

		}

		public static bool IsTall
		{
			get
			{
				return IsPhone && (UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale >= 1136);
			}
		}
	}
}

