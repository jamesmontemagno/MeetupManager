using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.ObjCRuntime;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using Cirrious.FluentLayouts.Touch;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.iOS.Views
{
	[Register("LoginView")]
	public partial class LoginView : BaseViewController
    {

		public LoginView() : base("LoginView", null)
		{
			this.Tag = "LoginView";
			this.Title = "Meetup Manager";
		}

        public override void ViewDidLoad()
        {
       
            base.ViewDidLoad();

			// ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
               EdgesForExtendedLayout = UIRectEdge.None;

			var refresh = new UIBarButtonItem (UIBarButtonSystemItem.Refresh);
			var info = new UIBarButtonItem ("About", UIBarButtonItemStyle.Plain, null);


			ButtonLogin.BackgroundColor = UIColor.Red;

			var set = this.CreateBindingSet<LoginView, LoginViewModel>();

			set.Bind (LabelLoginInfo).For("Visibility").To (v => v.IsBusy).WithConversion ("InvertedVisibility");
			set.Bind (ButtonLogin).For("Visibility").To (v => v.IsBusy).WithConversion ("InvertedVisibility");
			set.Bind (ActivityLoggingIn).For("Visibility").To (v => v.IsBusy).WithConversion ("Visibility");
			set.Bind (LabelLoggingIn).For("Visibility").To (v => v.IsBusy).WithConversion ("Visibility");
			set.Bind (ButtonLogin).To (vm => vm.LoginCommand);
			set.Bind (refresh).To (vm => vm.RefreshLoginCommand);
			set.Bind (info).To (vm => vm.ShowInfoCommand);
			set.Apply();


			((BaseViewModel)ViewModel).IsBusyChanged = (busy) => {
				if(busy)
					ActivityLoggingIn.StartAnimating();
				else
					ActivityLoggingIn.StopAnimating();
			};

			   

			NavigationItem.RightBarButtonItem = refresh;
			NavigationItem.LeftBarButtonItem = info;
		
        }
    }
}