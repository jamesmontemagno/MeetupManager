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

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new MvxSimpleTableViewSource(TableView,
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

			((GroupsViewModel)ViewModel).RefreshCommand.Execute (null);
		}
	}
}

