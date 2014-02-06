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
using Android.Content;
using Cirrious.CrossCore.Platform;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.Droid.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Droid.PlatformSpecific;
using Refractored.MvxPlugins.Settings;
using Refractored.MvxPlugins.Settings.Droid;

namespace MeetupManager.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
			return new Portable.App();
        }

		protected override void InitializeLastChance ()
		{
            base.InitializeLastChance ();
			Mvx.RegisterSingleton<IMessageDialog>(()=>new MessageDialog());
			Mvx.RegisterSingleton<ILogin>(()=>new MeetupLogin());
			Mvx.RegisterSingleton<ISettings> (() => new MvxAndroidSettings ());
        }


		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}