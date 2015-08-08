using Android.App;
using Android.Widget;
using MeetupManager.Portable.Interfaces;
using Android.Views;
using Xamarin.Forms;
using MeetupManager.Droid.PlatformSpecific;


[assembly:Dependency(typeof(MessageDialog))]
namespace MeetupManager.Droid.PlatformSpecific
{
    public class MessageDialog : IMessageDialog
    {

        public static void SendMessage(Activity activity, string message, string title = null)
        {
            var builder = new AlertDialog.Builder(activity);
            builder
            .SetTitle(title ?? string.Empty)
            .SetMessage(message)
            .SetPositiveButton(Resource.String.ok, delegate
                {

                });

            AlertDialog alert = builder.Create();
            alert.Show();
        }

        public void SendMessage(string message, string title = null)
        {
            var activity = Xamarin.Forms.Forms.Context as Activity;
            var builder = new AlertDialog.Builder(activity);
            builder
				.SetTitle(title ?? string.Empty)
				.SetMessage(message)
				.SetPositiveButton(Resource.String.ok, delegate
                {
				
                });             
			       
            AlertDialog alert = builder.Create();
            alert.Show();
        }


        public void SendToast(string message)
        {
            var activity = Xamarin.Forms.Forms.Context as Activity;   
            activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(activity, message, ToastLength.Long).Show();
                });
       
        }


        public void SendConfirmation(string message, string title, System.Action<bool> confirmationAction)
        {
            var activity = Xamarin.Forms.Forms.Context as Activity;
            var builder = new AlertDialog.Builder(activity);
            builder
            .SetTitle(title ?? string.Empty)
            .SetMessage(message)
            .SetPositiveButton(Resource.String.ok, delegate
                {
                    confirmationAction(true);
                }).SetNegativeButton(Resource.String.cancel, delegate
                {
                    confirmationAction(false);
                });

            AlertDialog alert = builder.Create();
            alert.Show();
        }

        public void AskForString(string message, string title, System.Action<string> returnString)
        {
            var activity = Xamarin.Forms.Forms.Context as Activity;
            var builder = new AlertDialog.Builder(activity);
            builder.SetIcon(Resource.Drawable.ic_launcher);
            builder.SetTitle(title ?? string.Empty);
            builder.SetMessage(message);
            var view = Android.Views.View.Inflate(activity, Resource.Layout.dialog_add_member, null);
            builder.SetView(view);

            var textBoxName = view.FindViewById<EditText>(Resource.Id.name);


            builder.SetCancelable(true);
            builder.SetNegativeButton(Resource.String.cancel, delegate
                {
                });//do nothign on cancel




            builder.SetPositiveButton(Resource.String.ok, delegate
                {

                    if (string.IsNullOrWhiteSpace(textBoxName.Text))
                        return;

                    returnString(textBoxName.Text.Trim());
                });


            var alertDialog = builder.Create();
            alertDialog.Show();
				

        }
    }
}