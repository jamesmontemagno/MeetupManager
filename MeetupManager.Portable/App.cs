
using Xamarin.Forms;
using MeetupManager.Portable.Views;

namespace MeetupManager.UI
{
    public class App : Application
    {
        public App()
        {
            MainPage = new NavigationPage(new LoginView())
                {
                    BarBackgroundColor = Color.FromHex("F44336"),
                    BarTextColor = Color.White
                };
        }
    }
}