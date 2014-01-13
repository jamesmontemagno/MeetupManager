using Cirrious.MvvmCross.WindowsPhone.Views;
using MeetupManager.Portable.ViewModels;

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
        }
    }
}