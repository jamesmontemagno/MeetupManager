// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace MeetupManager.iOS.Views
{
	partial class EventView
	{
		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem ButtonPickWinner { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIBarButtonItem ButtonRSVPNumber { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITableView MainTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (MainTableView != null) {
				MainTableView.Dispose ();
				MainTableView = null;
			}

			if (ButtonPickWinner != null) {
				ButtonPickWinner.Dispose ();
				ButtonPickWinner = null;
			}

			if (ButtonRSVPNumber != null) {
				ButtonRSVPNumber.Dispose ();
				ButtonRSVPNumber = null;
			}
		}

		public int RowsInSection (MonoTouch.UIKit.UITableView tableView, int section)
		{
			return MainTableView.Source.RowsInSection(tableView, section);
		}

		public MonoTouch.UIKit.UITableViewCell GetCell (MonoTouch.UIKit.UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{

			if (VM.CanLoadMore && !VM.IsBusy && (indexPath.Row == VM.Members.Count - 1))
				VM.LoadMoreCommand.Execute (null);

			return MainTableView.Source.GetCell (tableView, indexPath);
		}
		#if !DEBUG
		[MonoTouch.Foundation.Export ("tableView:canEditRowAtIndexPath:")]
		public bool CanEditRow (MonoTouch.UIKit.UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
		{
			return MainTableView.Source.CanEditRow (tableView, indexPath);
		}

		[Export ("tableView:commitEditingStyle:forRowAtIndexPath:")]
		public void CommitEditingStyle (MonoTouch.UIKit.UITableView tableView, MonoTouch.UIKit.UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			MainTableView.Source.CommitEditingStyle (tableView, editingStyle, indexPath);
		}
		#endif
	
	}
}
