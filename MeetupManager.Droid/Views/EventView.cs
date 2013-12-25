using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;

namespace MeetupManager.Droid.Views
{
	[Activity (Label = "EventView")]			
	public class EventView : MvxActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.EventView);
		}
	}
}

