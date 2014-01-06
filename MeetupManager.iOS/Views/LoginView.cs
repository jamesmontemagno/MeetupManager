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
	public partial class LoginView : MvxViewController
    {

		public LoginView() : base("LoginView", null)
		{
			this.Title = "Meetup Manager";
		}

        public override void ViewDidLoad()
        {
       
            base.ViewDidLoad();

			// ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
               EdgesForExtendedLayout = UIRectEdge.None;


			var set = this.CreateBindingSet<LoginView, LoginViewModel>();

			set.Bind (LabelLoginInfo).For("Visibility").To (v => v.IsBusy).WithConversion ("InvertedVisibility");
			set.Bind (ButtonLogin).For("Visibility").To (v => v.IsBusy).WithConversion ("InvertedVisibility");
			set.Bind (ActivityLoggingIn).For("Visibility").To (v => v.IsBusy).WithConversion ("Visibility");
			set.Bind (LabelLoggingIn).For("Visibility").To (v => v.IsBusy).WithConversion ("Visibility");
			set.Bind (ButtonLogin).To ("LoginCommand");
			set.Apply();


			((BaseViewModel)ViewModel).IsBusyChanged = (busy) => {
				if(busy)
					ActivityLoggingIn.StartAnimating();
				else
					ActivityLoggingIn.StopAnimating();
			};

			   
			/*var meetupIcon = new UIImageView(UIImage.FromBundle ("ic_meetup"));
			meetupIcon.ContentMode = UIViewContentMode.ScaleAspectFit;
			Add (meetupIcon);

			var labelLogin = new UILabel();
			labelLogin.Text = NSBundle.MainBundle.LocalizedString ("LoginMessage", "Message to login");
			labelLogin.Lines = 2;
			Add(labelLogin);
           
			var loginButton = new UIButton ();
			loginButton.SetTitleColor (UIColor.Black, UIControlState.Normal);
			loginButton.SetTitle (NSBundle.MainBundle.LocalizedString ("Login", "Login"), UIControlState.Normal);
			Add (loginButton);

			var progressBar = new UIActivityIndicatorView (UIActivityIndicatorViewStyle.Gray);
			Add (progressBar);

			var labelLoggingIn = new UILabel();
			labelLoggingIn.Text = NSBundle.MainBundle.LocalizedString ("LoggingIn", "Logging in to meetup.com");
			Add(labelLoggingIn);

			this.View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

			var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            
			set.Bind (labelLogin).For("Visibility").To (v => v.IsBusy).WithConversion ("InvertedVisibility");
			set.Bind (loginButton).For("Visibility").To (v => v.IsBusy).WithConversion ("InvertedVisibility");
			set.Bind (progressBar).For("Visibility").To (v => v.IsBusy).WithConversion ("Visibility");
			set.Bind (labelLoggingIn).For("Visibility").To (v => v.IsBusy).WithConversion ("Visibility");
			set.Bind (loginButton).To ("LoginCommand");
            set.Apply();
		

			const int padding = 20;
			const int paddingSmall = 10;
			this.View.AddConstraints(
				meetupIcon.AtLeftOf(this.View, padding),
				meetupIcon.AtRightOf(this.View, padding),
				meetupIcon.AtTopOf(this.View, padding),
				meetupIcon.WithRelativeWidth(this.View, .8f),
				meetupIcon.WithRelativeHeight(this.View, .25f),

				labelLogin.Below(meetupIcon, paddingSmall),
				labelLogin.AtRightOf(this.View, padding),
				labelLogin.AtLeftOf(this.View, padding),

				loginButton.Below(labelLogin, paddingSmall),
				loginButton.AtRightOf(this.View, padding),
				loginButton.AtLeftOf(this.View, padding),

				progressBar.Below(loginButton, paddingSmall),
				progressBar.AtRightOf(this.View, padding),
				progressBar.AtLeftOf(this.View, padding),

				labelLoggingIn.Below(progressBar, paddingSmall),
				labelLoggingIn.WithSameLeft(labelLogin),
				labelLoggingIn.WithSameRight(labelLogin)
			);*/
		
        }
    }
}