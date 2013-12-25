using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Droid.Views
{
	[Activity(Label = "Events")]
	public class EventsView : MvxActivity
	{
		private EventsViewModel viewModel;
		private new EventsViewModel ViewModel 
		{
			get { return viewModel ?? (viewModel = base.ViewModel as EventsViewModel); }
		}
		protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SetContentView(Resource.Layout.EventsView);

			AndroidHUD.AndHUD.Shared.Show (this, "Loading...");
			await ViewModel.ExecuteRefreshCommand();
			AndroidHUD.AndHUD.Shared.Dismiss (this);
        }
    }
}