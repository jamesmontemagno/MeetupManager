using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces;
using MeetupManager.iOS.PlatformSpecific;

namespace MeetupManager.iOS
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

		protected override IMvxApplication CreateApp ()
		{
			return new Portable.App();
		}

		protected override void InitializeLastChance ()
		{
			base.InitializeLastChance ();
			Mvx.RegisterSingleton<IMessageDialog>(()=>new MessageDialog());
			Mvx.RegisterSingleton<ILogin> (() => new MeetupLogin ());
			Mvx.RegisterSingleton<IDeserialize> (() => new Deserialize ());
		}
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
	}
}