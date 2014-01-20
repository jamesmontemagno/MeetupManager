using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using MeetupManager.Portable.Interfaces;
using MeetupManager.WP8.PlatformSpecific;
using Microsoft.Phone.Controls;
using Refractored.MvxPlugins.Settings;
using Refractored.MvxPlugins.Settings.WindowsPhone;

namespace MeetupManager.WP8
{
    public class Setup : MvxPhoneSetup
    {
        public Setup(PhoneApplicationFrame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Portable.App();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.RegisterSingleton<IMessageDialog>(() => new WP8MessageDialog());
            Mvx.RegisterSingleton<ILogin>(() => new WP8MeetupLogin());
            Mvx.RegisterSingleton<ISettings>(() => new MvxWindowsPhoneSettings());
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}