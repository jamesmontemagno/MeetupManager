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
using Android.Content;
using Android.OS;
using Cirrious.CrossCore.Droid.Views;
using Android.Support.V7.App;
using Cirrious.CrossCore.Core;

namespace MeetupManager.Droid.Helpers
{
	public class MvxActionBarEventSourceActivity : ActionBarActivity
	, IMvxEventSourceActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			CreateWillBeCalled.Raise(this, bundle);
			base.OnCreate(bundle);
			CreateCalled.Raise(this, bundle);
		}

		protected override void OnDestroy()
		{
			DestroyCalled.Raise(this);
			base.OnDestroy();
		}

		protected override void OnNewIntent(Intent intent)
		{
			base.OnNewIntent(intent);
			NewIntentCalled.Raise(this, intent);
		}

		protected override void OnResume()
		{
			base.OnResume();
			ResumeCalled.Raise(this);
		}

		protected override void OnPause()
		{
			PauseCalled.Raise(this);
			base.OnPause();
		}

		protected override void OnStart()
		{
			base.OnStart();
			StartCalled.Raise(this);
		}

		protected override void OnRestart()
		{
			base.OnRestart();
			RestartCalled.Raise(this);
		}

		protected override void OnStop()
		{
			StopCalled.Raise(this);
			base.OnStop();
		}

		public override void StartActivityForResult(Intent intent, int requestCode)
		{
			StartActivityForResultCalled.Raise(this, new MvxStartActivityForResultParameters(intent, requestCode));
			base.StartActivityForResult(intent, requestCode);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			ActivityResultCalled.Raise(this, new MvxActivityResultParameters(requestCode, resultCode, data));
			base.OnActivityResult(requestCode, resultCode, data);
		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			SaveInstanceStateCalled.Raise(this, outState);
			base.OnSaveInstanceState(outState);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				DisposeCalled.Raise(this);
			}
			base.Dispose(disposing);
		}

		public event EventHandler DisposeCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> CreateWillBeCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> CreateCalled;
		public event EventHandler DestroyCalled;
		public event EventHandler<MvxValueEventArgs<Intent>> NewIntentCalled;
		public event EventHandler ResumeCalled;
		public event EventHandler PauseCalled;
		public event EventHandler StartCalled;
		public event EventHandler RestartCalled;
		public event EventHandler StopCalled;
		public event EventHandler<MvxValueEventArgs<Bundle>> SaveInstanceStateCalled;
		public event EventHandler<MvxValueEventArgs<MvxStartActivityForResultParameters>> StartActivityForResultCalled;
		public event EventHandler<MvxValueEventArgs<MvxActivityResultParameters>> ActivityResultCalled;
	}
}

