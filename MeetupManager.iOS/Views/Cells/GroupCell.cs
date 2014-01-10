using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MeetupManager.Portable.Models;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace MeetupManager.iOS.Views
{
	public partial class GroupCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("GroupCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("GroupCell");

		private readonly MvxImageViewLoader _imageViewLoader;

		public GroupCell (IntPtr handle) : base (handle)
		{
			_imageViewLoader = new MvxImageViewLoader(() => this.ImageGroupPhoto);

			this.DelayBind (() => {
				var set = this.CreateBindingSet<GroupCell, Group> ();
				set.Bind (LabelName).To (g => g.Name);
				set.Bind (_imageViewLoader).To (g => g.GroupPhoto.ThumbLink);
				set.Apply ();
			});
		}
	

		public static GroupCell Create ()
		{
			return (GroupCell)Nib.Instantiate (null, null) [0];
		}
	}
}

