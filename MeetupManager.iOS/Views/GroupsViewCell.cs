using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MeetupManager.iOS.Views
{
	public class GroupsViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("GroupsViewCell");

		[Export ("initWithFrame:")]
		public GroupsViewCell (RectangleF frame) : base (frame)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			BackgroundColor = UIColor.Cyan;
		}
	}
}

