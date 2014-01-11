/*
 * MeetupManager:
 * Copyright (C) 2013 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using Android.App;
using Android.Widget;
using MeetupManager.Portable.Interfaces;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Droid.Platform;
using Android.Views;

namespace MeetupManager.Droid.PlatformSpecific
{
	public class MessageDialog : IMessageDialog
  {
    public void SendMessage(string message, string title = null)
    {
			var builder = new AlertDialog.Builder(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
			builder
				.SetTitle(title ?? string.Empty)
				.SetMessage(message)
				.SetPositiveButton(Resource.String.ok, delegate {
				
			});             
			       
			AlertDialog alert = builder.Create();
			alert.Show();
    }


    public void SendToast(string message)
    {
        Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity.RunOnUiThread(() =>
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        });
       
    }


    public void SendConfirmation(string message, string title, System.Action<bool> confirmationAction)
    {
        var builder = new AlertDialog.Builder(Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
        builder
            .SetTitle(title ?? string.Empty)
            .SetMessage(message)
            .SetPositiveButton(Resource.String.ok, delegate
            {
                confirmationAction(true);
            }).SetNegativeButton(Resource.String.cancel, delegate { confirmationAction(false); });

        AlertDialog alert = builder.Create();
        alert.Show();
    }

		public void AskForString (string message, string title, System.Action<string> returnString)
		{
			var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity> ().Activity;
		    var builder = new AlertDialog.Builder(activity);
		    builder.SetIcon(Resource.Drawable.ic_launcher);
		    builder.SetTitle(title ?? string.Empty);
		    builder.SetMessage(message);
		    var view = View.Inflate(activity, Resource.Layout.dialog_add_member, null);
		    builder.SetView(view);

		    var textBoxName = view.FindViewById<EditText>(Resource.Id.name);


		    builder.SetCancelable(true);
		    builder.SetNegativeButton(Resource.String.cancel, delegate { });//do nothign on cancel




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