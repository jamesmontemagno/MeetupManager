using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace MeetupManager.iOS.Views
{
	public class GroupsView : MvxCollectionViewController
	{
		public GroupsView(UICollectionViewLayout layout) : base (layout)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Register any custom UICollectionViewCell classes
			CollectionView.RegisterClassForCell (typeof(GroupsViewCell), GroupsViewCell.Key);
			
			// Note: If you use one of the Collection View Cell templates to create a new cell type,
			// you can register it using the RegisterNibForCell() method like this:
			//
			// CollectionView.RegisterNibForCell (MyCollectionViewCell.Nib, MyCollectionViewCell.Key);
			/*var set = this.CreateBindingSet<GroupsView, GroupsViewModel>();
			set.Bind(label).To(vm => vm.Hello);
			set.Bind(textField).To(vm => vm.Hello);
			set.Apply();*/

		}

		public override int NumberOfSections (UICollectionView collectionView)
		{
			// TODO: return the actual number of sections
			return 1;
		}

		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			// TODO: return the actual number of items in the section
			return 1;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.DequeueReusableCell (GroupsViewCell.Key, indexPath) as GroupsViewCell;
			
			// TODO: populate the cell with the appropriate data based on the indexPath
			
			return cell;
		}
	}
}

