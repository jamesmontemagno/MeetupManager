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
		private EventsViewModel viewModel;
		public EventsViewModel VM {get { return viewModel ?? (viewModel = base.ViewModel as EventsViewModel);}}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new MySource(VM, TableView,
				EventCell.Key, EventCell.Key);
			TableView.RowHeight = 66;
			TableView.Source = source;

			var refreshControl = new MvxUIRefreshControl{Message = string.Empty};
			this.RefreshControl = refreshControl;

			var set = this.CreateBindingSet<EventsView, EventsViewModel>();
			set.Bind(source).To(vm => vm.Events);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.GoToEventCommand);
			set.Apply();
			var spinner = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
			spinner.Frame = new RectangleF (0, 0, 320, 66);

			TableView.TableFooterView = spinner;
			//if isn't first load show spinner when busy
			VM.IsBusyChanged = (busy) => {
				if(busy && VM.Events.Count > 0)
					spinner.StartAnimating();
				else
					spinner.StopAnimating();
			};
			TableView.ReloadData();
			NavigationItem.RightBarButtonItem = new UIBarButtonItem ("Stats", UIBarButtonItemStyle.Plain, delegate {
				((EventsViewModel)ViewModel).ExecuteGoToStatsCommand();
			});

		}
		///Custom source so when we scroll to bottom
		public class MySource : MvxSimpleTableViewSource
		{
			EventsViewModel vm;
			public MySource(EventsViewModel vm, UITableView tableView, NSString key, NSString cellId) : base(tableView, key, cellId)
			{
				this.vm = vm;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				if (vm.CanLoadMore && !vm.IsBusy && (indexPath.Row == vm.Events.Count - 1))
					vm.LoadMoreCommand.Execute (null);

				return base.GetCell (tableView, indexPath);
			}
		}

	}
}

