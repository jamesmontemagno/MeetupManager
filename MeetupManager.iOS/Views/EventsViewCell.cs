using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MeetupManager.iOS.Views
{
	public class EventsViewCell : UICollectionViewCell
	{
		public static readonly NSString Key = new NSString ("EventsViewCell");

		[Export ("initWithFrame:")]
		public EventsViewCell (RectangleF frame) : base (frame)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			BackgroundColor = UIColor.Cyan;
		}
	}
}

