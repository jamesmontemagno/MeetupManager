using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MeetupManager.Portable.ViewModels;
using MeetupManager.iOS.PlatformSpecific;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace MeetupManager.iOS.Views
{
	public class EventsView : BaseTableViewController
	{
		public EventsView() : base(UITableViewStyle.Plain)
		{
			this.Tag = "EventsView";
			this.Title = "Events";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new MvxStandardTableViewSource(TableView,
				UITableViewCellStyle.Subtitle,
				new NSString("group_id"),
				"TitleText Name;DetailText MonthDayYear",
				UITableViewCellAccessory.DisclosureIndicator);

			TableView.Source = source;

			var refreshControl = new MvxUIRefreshControl{Message = "Loading..."};
			this.RefreshControl = refreshControl;

			var set = this.CreateBindingSet<EventsView, EventsViewModel>();
			set.Bind(source).To(vm => vm.Events);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.GoToEventCommand);
			set.Apply();

			TableView.ReloadData();


		}
	}
}

