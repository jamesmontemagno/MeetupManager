using Android.App;
using Android.Widget;
using MeetupManager.Portable.Interfaces;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid;

namespace MeetupManager.Droid.PlatformSpecific
{
	public class MessageDialog : IMessageDialog
  {
    public void SendMessage(string message, string title = null)
    {
			var builder = new AlertDialog.Builder(Mvx.Resolve<IMvxAndroidGlobals>().ApplicationContext);
			builder
				.SetTitle(title ?? string.Empty)
				.SetMessage(message)
				.SetPositiveButton("Yes", delegate {
				
			});             
			       
			AlertDialog alert = builder.Create();
			alert.Show();
    }
  }
}