using System;
using MeetupManager.Portable.Interfaces;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Cirrious.MvvmCross.Binding.Touch.Views;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Bindings;

namespace MeetupManager.iOS.PlatformSpecific
{
	public class MvxDeleteSimpleTableViewSource : MvxSimpleTableViewSource
	{

		private IRemove m_ViewModel;


		#region Constructors
		public MvxDeleteSimpleTableViewSource(IRemove viewModel, IntPtr intptr) 
			: base(intptr)
		{
			m_ViewModel = viewModel;
		}


		public MvxDeleteSimpleTableViewSource(IRemove viewModel, UITableView tableView, string nibName, string cellIdentifier = null, NSBundle bundle = null) 
			: base(tableView, nibName, cellIdentifier, bundle)
		{
			m_ViewModel = viewModel;
		}

		public MvxDeleteSimpleTableViewSource(IRemove viewModel, UITableView tableView, Type cellType, string cellIdentifier = null) 
			: base(tableView, cellType, cellIdentifier)
		{
			m_ViewModel = viewModel;
		}

		#endregion

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return m_ViewModel.CanRemove(indexPath.Row);
		}


		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
			case UITableViewCellEditingStyle.Delete:
				m_ViewModel.RemoveCommand.Execute (indexPath.Row);
				break;
			case UITableViewCellEditingStyle.None:
				break;
			}
		}

		public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return UITableViewCellEditingStyle.Delete;
		}

		public override bool CanMoveRow(UITableView tableView, NSIndexPath indexPath)
		{
			return false;
		}


	}
}

