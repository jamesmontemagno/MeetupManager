//#define MOCK
using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Services.Database;
using Cirrious.MvvmCross.Community.Plugins.Sqlite;


namespace MeetupManager.Portable
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
			string service = "Service";
			#if MOCK
			service = "ServiceMock";
			#endif

            CreatableTypes()
				.EndingWith(service)
                .AsInterfaces()
                .RegisterAsLazySingleton();

			#if MOCK
			Mvx.RegisterSingleton<IDataService>(()=>new DataService(Mvx.Resolve<ISQLiteConnectionFactory>()));
			#endif
				
			RegisterAppStart<ViewModels.EventsViewModel>();
        }
    }
}