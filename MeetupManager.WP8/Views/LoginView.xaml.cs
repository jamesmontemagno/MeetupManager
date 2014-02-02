using Cirrious.CrossCore;
using Cirrious.MvvmCross.WindowsPhone.Views;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.ViewModels;
using MeetupManager.WP8.PlatformSpecific;

namespace MeetupManager.WP8.Views
{
    public partial class LoginView : MvxPhonePage
    {
        public new LoginViewModel ViewModel
        {
            get { return (LoginViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public LoginView()
        {
            InitializeComponent();
            var login = Mvx.Resolve<ILogin>() as WP8MeetupLogin;
            login.Browser = Browser;
        }
    }
}