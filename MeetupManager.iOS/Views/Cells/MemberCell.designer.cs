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
	[Register ("MemberCell")]
	partial class MemberCell
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView ImageCheckedIn { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView ImagePhoto { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LabelName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LabelName != null) {
				LabelName.Dispose ();
				LabelName = null;
			}

			if (ImageCheckedIn != null) {
				ImageCheckedIn.Dispose ();
				ImageCheckedIn = null;
			}

			if (ImagePhoto != null) {
				ImagePhoto.Dispose ();
				ImagePhoto = null;
			}
		}
	}
}
