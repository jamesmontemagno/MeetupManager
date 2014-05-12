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
	public partial class EventView : BaseViewController, IUITableViewDataSource
	{
		public EventView () : base ("EventView", null)
		{
			this.Title = "Event";
			this.Tag = "EventView";
		}

		private EventViewModel viewModel;
		public EventViewModel VM {get { return viewModel ?? (viewModel = base.ViewModel as EventViewModel);}}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var addNewMemberButton = new UIBarButtonItem (UIBarButtonSystemItem.Add);

			var source = new MvxDeleteSimpleTableViewSource((IRemove)ViewModel, MainTableView, MemberCell.Key, MemberCell.Key);
			MainTableView.RowHeight = 66;
			MainTableView.Source = source;
			MainTableView.WeakDataSource = this;

			var refreshControl = new MvxUIRefreshControl{Message = string.Empty};
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
			var spinner = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
			spinner.Frame = new RectangleF (0, 0, 320, 66);

			MainTableView.TableFooterView = spinner;
			VM.IsBusyChanged = (busy) => {
				if(busy && VM.Members.Count > 0)
					spinner.StartAnimating();
				else
					spinner.StopAnimating();
			};
			NavigationItem.RightBarButtonItem = addNewMemberButton;

			//MainTableView.Source.
		}


	}
}

