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
	[Register ("EventCell")]
	partial class EventCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel LabelMonthDay { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LabelName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LabelYear { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LabelName != null) {
				LabelName.Dispose ();
				LabelName = null;
			}

			if (LabelMonthDay != null) {
				LabelMonthDay.Dispose ();
				LabelMonthDay = null;
			}

			if (LabelYear != null) {
				LabelYear.Dispose ();
				LabelYear = null;
			}
		}
	}
}
