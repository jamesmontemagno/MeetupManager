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
	partial class EventView
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem ButtonPickWinner { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem ButtonRSVPNumber { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView MainTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MainTableView != null) {
				MainTableView.Dispose ();
				MainTableView = null;
			}

			if (ButtonPickWinner != null) {
				ButtonPickWinner.Dispose ();
				ButtonPickWinner = null;
			}

			if (ButtonRSVPNumber != null) {
				ButtonRSVPNumber.Dispose ();
				ButtonRSVPNumber = null;
			}
		}
	}
}
