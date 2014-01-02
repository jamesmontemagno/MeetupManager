using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MeetupManager.iOS.Views
{
	public class EventViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("EventViewCell");

		[Export ("initWithFrame:")]
		public EventViewCell (RectangleF frame) : base (frame)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			BackgroundColor = UIColor.Cyan;
		}
	}
}

