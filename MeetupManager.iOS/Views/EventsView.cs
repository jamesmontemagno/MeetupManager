using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.iOS.Views
{
	public class EventsView : MvxTableViewController
	{
		public EventsView() : base(UITableViewStyle.Plain)
		{
			this.Title = "Events";
		}
	}
}

