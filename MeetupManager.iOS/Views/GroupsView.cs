using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MeetupManager.Portable.ViewModels;
using MeetupManager.iOS.PlatformSpecific;

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

			var source = new MvxStandardTableViewSource(TableView, "TitleText Name;ImageUrl GroupPhoto.ThumbLink");
			TableView.Source = source;

			var refreshControl = new MvxUIRefreshControl{Message = "Loading..."};
			this.RefreshControl = refreshControl;




			var set = this.CreateBindingSet<GroupsView, GroupsViewModel>();
			set.Bind(source).To(vm => vm.Groups);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To("RefreshCommand");
			set.Apply();

			TableView.ReloadData();
		}
	}
}

