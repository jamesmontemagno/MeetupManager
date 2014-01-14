using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.iOS.Views
{
	public partial class MemberCell : MvxTableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("MemberCell", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("MemberCell");

		private readonly MvxImageViewLoader _imageViewLoader;

		public MemberCell (IntPtr handle) : base (handle)
		{
			_imageViewLoader = new MvxImageViewLoader(() => this.ImagePhoto);

			this.DelayBind (() => {
				var set = this.CreateBindingSet<MemberCell, MemberViewModel> ();
				set.Bind (LabelName).To (member => member.Name);
				set.Bind (_imageViewLoader).To (member => member.Photo.ThumbLink);
				set.Bind(ImageCheckedIn).For("Visibility").To(member=>member.CheckedIn).WithConversion("Visibility");
				set.Apply ();
			});
		}

		public static MemberCell Create ()
		{
			return (MemberCell)Nib.Instantiate (null, null) [0];
		}
	}
}

