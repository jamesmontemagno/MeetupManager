
using MonoTouch.UIKit;
using MeetupManager.Portable.Interfaces;
using GCDiscreetNotification;

namespace MeetupManager.iOS.PlatformSpecific
{
  public class MessageDialog : IMessageDialog
  {

    public void SendMessage(string message, string title = null)
    {
			Helpers.EnsureInvokedOnMainThread (() => {
				var alertView = new UIAlertView (title ?? string.Empty, message, null, "OK");
				alertView.Show ();
			});
    }


    public void SendToast(string message)
    {
			var notificationView = new GCDiscreetNotificationView(
				text: message,
				activity: false,
				presentationMode: GCDNPresentationMode.Bottom,
				view: UIApplication.SharedApplication.KeyWindow
			);

			notificationView.ShowAndDismissAfter(4);
    }

	public void SendConfirmation (string message, string title, System.Action<bool> confirmationAction)
	{
			Helpers.EnsureInvokedOnMainThread (() => {
				var alertView = new UIAlertView (title ?? string.Empty, message, null, "OK", "Cancel");
				alertView.Clicked += (sender, e) => {
					confirmationAction (e.ButtonIndex == 0);
				};
				alertView.Show ();
			});
	}

	public void AskForString (string message, string title, System.Action<string> returnString)
	{
		Helpers.EnsureInvokedOnMainThread (() => {
			var alertView = new UIAlertView (title ?? string.Empty, message, null, "OK", "Cancel");
			alertView.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
			alertView.Clicked += (sender, e) => {
				var text = alertView.GetTextField(0).Text.Trim();
				if(e.ButtonIndex == 0 && !string.IsNullOrWhiteSpace(text))
					returnString (text);
			};
			alertView.Show ();
		});
	}
  }
}