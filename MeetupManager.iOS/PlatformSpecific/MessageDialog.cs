
using MonoTouch.UIKit;
using MeetupManager.Portable.Interfaces;

namespace MeetupManager.iOS.PlatformSpecific
{
  public class MessageDialog : IMessageDialog
  {

    public void SendMessage(string message, string title = null)
    {
		var alertView = new UIAlertView (title ?? string.Empty, message, null, "OK");
		alertView.Show ();
    }
  }
}