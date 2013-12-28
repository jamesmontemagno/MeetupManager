using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Droid.PlatformSpecific;

namespace MeetupManager.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
			return new Portable.App();
        }

		protected override void InitializeLastChance ()
		{
			base.InitializeLastChance ();
			Mvx.RegisterSingleton<IMessageDialog>(()=>new MessageDialog());
			Mvx.RegisterSingleton<ILogin>(()=>new MeetupLogin());
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}