using Cirrious.CrossCore.IoC;

namespace MeetupManager.Portable
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
				
			RegisterAppStart<ViewModels.EventsViewModel>();
        }
    }
}