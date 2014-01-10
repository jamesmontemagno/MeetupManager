using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MeetupManager.iOS.PlatformSpecific;
using MeetupManager.Portable.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;
using MeetupManager.Portable.Interfaces;

namespace MeetupManager.iOS.Views
{
	[Register("EventView")]
	public partial class EventView : BaseViewController
	{
		public EventView () : base ("EventView", null)
		{
			this.Title = "Event";
			this.Tag = "EventView";
		}


		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var addNewMemberButton = new UIBarButtonItem (UIBarButtonSystemItem.Add);

			var source = new MvxDeleteSimpleTableViewSource((IRemove)ViewModel, MainTableView, MemberCell.Key, MemberCell.Key);
			MainTableView.RowHeight = 66;
			MainTableView.Source = source;

			var refreshControl = new MvxUIRefreshControl{Message = "Loading..."};
			MainTableView.AddSubview (refreshControl);

			var set = this.CreateBindingSet<EventView, EventViewModel>();
			set.Bind(source).To(vm => vm.Members);
			set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.CheckInCommand);
			set.Bind(refreshControl).For(r => r.IsRefreshing).To(vm => vm.IsBusy);
			set.Bind(refreshControl).For(r => r.RefreshCommand).To(vm => vm.RefreshCommand);
			set.Bind (this).For("Title").To (vm => vm.EventName);
			set.Bind (ButtonPickWinner).To (vm => vm.SelectWinnerCommand);
			set.Bind (ButtonRSVPNumber).For ("Title").To (vm => vm.RSVPCount);
			set.Bind (addNewMemberButton).To (vm => vm.AddNewUserCommand);
			set.Apply();

			MainTableView.ReloadData();

			NavigationItem.RightBarButtonItem = addNewMemberButton;
		}
	}
}

