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
using System;
using Android.App;
using MeetupManager.Droid.Helpers;
using MeetupManager.Portable.ViewModels;
using Android.OS;

namespace MeetupManager.Droid.Views
{
	[Activity (Label = "Login")]			
	public class LoginView : MvxActionBarActivity
	{
		private LoginViewModel viewModel;
		private new LoginViewModel ViewModel 
		{
			get { return viewModel ?? (viewModel = base.ViewModel as LoginViewModel); }
		}

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.view_login);
		}
	}
}

