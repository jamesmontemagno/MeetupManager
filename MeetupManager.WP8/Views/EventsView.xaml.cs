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
using System.Windows.Navigation;
using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Views;

namespace MeetupManager.WP8.Views
{
    public partial class EventsView : MvxPhonePage
    {
      public new EventsViewModel ViewModel
      {
          get { return (EventsViewModel)base.ViewModel; }
          set { base.ViewModel = value; }
      }

      public EventsView()
      {
          InitializeComponent();
      }

      private void AppBarRefreshClick(object sender, EventArgs e)
      {
          this.ViewModel.RefreshCommand.Execute();
      }

      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
        base.OnNavigatedTo(e);
        AppBarFilerEvents.Text = Settings.ShowAllEvents ? "show recent events" : "show all events";
      }

      private void AppBarFilerEvents_OnClick(object sender, EventArgs e)
      {
        if (ViewModel.IsBusy)
          return;

        Settings.ShowAllEvents = !Settings.ShowAllEvents;
        this.ViewModel.RefreshCommand.Execute();
        AppBarFilerEvents.Text = Settings.ShowAllEvents ? "show recent events" : "show all events";
      }
    }
}