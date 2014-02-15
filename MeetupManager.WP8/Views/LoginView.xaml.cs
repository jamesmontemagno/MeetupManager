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
 * 
 * Help from Ed Snider http://twitter.com/EdSnider
 */

using System;
using System.Windows;
using System.Windows.Navigation;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.WindowsPhone.Views;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.ViewModels;
using MeetupManager.WP8.PlatformSpecific;
using Microsoft.Phone.Controls;

namespace MeetupManager.WP8.Views
{
    public partial class LoginView : MvxPhonePage
    {
        public new LoginViewModel ViewModel
        {
            get { return (LoginViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public LoginView()
        {
            InitializeComponent();
            var login = Mvx.Resolve<ILogin>() as WP8MeetupLogin;
          
            login.Browser = Browser;
          
        }

      protected async override void OnNavigatedTo(NavigationEventArgs e)
      {
        base.OnNavigatedTo(e);
        await Browser.ClearCookiesAsync();
        await Browser.ClearInternetCacheAsync();
      }

      private void AppBarAbout_OnClick(object sender, EventArgs e)
      {
        MessageBox.Show("Created by @JamesMontemagno Copyright 2014 Refractored LLC. Open source and created completely in C# with Xamarin! V 1.0.0.0", "About",
          MessageBoxButton.OK);
      }
    }
}