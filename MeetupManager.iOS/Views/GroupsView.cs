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
	public partial class GroupsView : MvxTableViewController
	{
		public GroupsView() : base (UITableViewStyle.Plain)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var source = new MvxStandardTableViewSource(TableView,
				UITableViewCellStyle.Default,
				 new NSString("group_id"),
				 "TitleText Name;ImageUrl GroupPhoto.ThumbLink",
				 UITableViewCellAccessory.DisclosureIndicator);

			TableView.Source = source;

			var refreshControl = new MvxUIRefreshControl{Message = "Loading..."};
			this.RefreshControl = refreshControl;

			var set = this.CreateBindingSet<GroupsView, GroupsViewModel>();
			set.Bind(source).To(vm => vm.Groups);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To("RefreshCommand");
			set.Bind (source).For ("SelectedItemChanged").To ("GoToGroupCommand");
			set.Apply();

			TableView.ReloadData();

			((GroupsViewModel)ViewModel).RefreshCommand.Execute (null);
		}
	}
}

