using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MeetupManager.Portable.Models;

namespace MeetupManager.iOS.Views
{
	public partial class EventCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("EventCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("EventCell");

		public EventCell (IntPtr handle) : base (handle)
		{
			this.DelayBind (() => {
				var set = this.CreateBindingSet<EventCell, Event> ();
				set.Bind (LabelName).To (g => g.Name);
				set.Bind (LabelYear).To(g => g.Year);
				set.Bind(LabelMonthDay).To(g => g.MonthDay);
				set.Apply ();
			});
		}

		public static EventCell Create ()
		{
			return (EventCell)Nib.Instantiate (null, null) [0];
		}
	}
}

