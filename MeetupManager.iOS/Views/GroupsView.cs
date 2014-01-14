using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MeetupManager.Portable.ViewModels;
using MeetupManager.iOS.PlatformSpecific;
using System.Windows.Input;

namespace MeetupManager.iOS.Views
{
	public partial class GroupsView : BaseTableViewController
	{
		public GroupsView() : base (UITableViewStyle.Plain)
		{
			this.Tag = "GroupsView";
			this.Title = "Groups";
		}

		private GroupsViewModel viewModel;
		public GroupsViewModel VM {get { return viewModel ?? (viewModel = base.ViewModel as GroupsViewModel);}}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new MySource(VM, TableView,
				GroupCell.Key, GroupCell.Key);
			TableView.RowHeight = 66;
			TableView.Source = source;


			var refreshControl = new MvxUIRefreshControl{Message = "Loading..."};
			this.RefreshControl = refreshControl;

			var set = this.CreateBindingSet<GroupsView, GroupsViewModel>();
			set.Bind(source).To(vm => vm.Groups);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.GoToGroupCommand);
			set.Apply();

			TableView.ReloadData();
			var spinner = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
			spinner.Frame = new RectangleF (0, 0, 320, 66);
			//if isn't first time show spinner when busy
			TableView.TableFooterView = spinner;
			VM.IsBusyChanged = (busy) => {
				if(busy && VM.Groups.Count > 0)
					spinner.StartAnimating();
				else
					spinner.StopAnimating();
			};
			VM.RefreshCommand.Execute (null);
		}

		//custom source so we can load more when bottom is hit
		public class MySource : MvxSimpleTableViewSource
		{
			GroupsViewModel vm;
			public MySource(GroupsViewModel vm, UITableView tableView, NSString key, NSString cellId) : base(tableView, key, cellId)
			{
				this.vm = vm;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				if (vm.CanLoadMore && !vm.IsBusy && (indexPath.Row == vm.Groups.Count - 1))
					vm.LoadMoreCommand.Execute (null);

				return base.GetCell (tableView, indexPath);
			}
		}

	}
}

