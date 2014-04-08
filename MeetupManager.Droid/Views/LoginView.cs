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
using MeetupManager.Portable.ViewModels;
using Android.OS;

namespace MeetupManager.Droid.Views
{
    [Activity(Label = "Login", Icon = "@drawable/ic_launcher", Theme="@style/Theme")]
    public class LoginView : BaseView
	{
		private LoginViewModel viewModel;
		private new LoginViewModel ViewModel 
		{
			get { return viewModel ?? (viewModel = base.ViewModel as LoginViewModel); }
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

            Tag = "Login";
			SetContentView(Resource.Layout.view_login);
		}


        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_login, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            if (ViewModel.IsBusy)
                return base.OnOptionsItemSelected(item);

            switch (item.ItemId)
            {
                case Resource.Id.menu_about:
                    var builder = new AlertDialog.Builder(this);
			            builder
				        .SetTitle(Resource.String.menu_about)
				        .SetMessage(Resource.String.about)
				        .SetPositiveButton(Resource.String.ok, delegate {
				
			        });             
			       
			        AlertDialog alert = builder.Create();
			        alert.Show();
                    return true;
                case Resource.Id.menu_refresh:
                    ViewModel.RefreshLoginCommand.Execute(null);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
	}
}

