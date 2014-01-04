using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MeetupManager.Droid.Helpers
{
    public static class PopupHelpers
    {
        public static void ShowNewUserPopup(Activity activity, Action<string> callback)
        {
            var builder = new AlertDialog.Builder(activity);
            builder.SetIcon(Resource.Drawable.ic_launcher);
            builder.SetTitle(Resource.String.new_member);
            var view = View.Inflate(activity, Resource.Layout.dialog_add_member, null);
            builder.SetView(view);

            var textBoxName = view.FindViewById<EditText>(Resource.Id.name);


            builder.SetCancelable(true);
            builder.SetNegativeButton(Resource.String.cancel, delegate { });//do nothign on cancel




            builder.SetPositiveButton(Resource.String.ok, delegate
            {

                if (string.IsNullOrWhiteSpace(textBoxName.Text))
                    return;

                callback(textBoxName.Text.Trim());
            });

            activity.RunOnUiThread(() =>
            {
                var alertDialog = builder.Create();
                alertDialog.Show();
            });
        }
    }
}