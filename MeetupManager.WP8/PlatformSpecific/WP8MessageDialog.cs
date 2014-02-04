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
using System.Windows.Controls;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Coding4Fun.Toolkit.Controls;
using MeetupManager.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MeetupManager.WP8.PlatformSpecific
{
    public class WP8MessageDialog : IMessageDialog
    {
        public void SendMessage(string message, string title = null)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(message, title?? string.Empty, MessageBoxButton.OK);
            });
        }

        public void SendToast(string message)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                var toast = new ToastPrompt();
                toast.Message = message;
                toast.Show();
            });
        }

        public void SendConfirmation(string message, string title, Action<bool> confirmationAction)
        {
            var result = MessageBox.Show(message, title??string.Empty, MessageBoxButton.OKCancel);
            confirmationAction(result == MessageBoxResult.OK);
        }

        public void AskForString(string message, string title, Action<string> returnString)
        {
            var input = new InputPrompt();
            input.Completed += (sender, args) =>
            {
                if(args.PopUpResult == PopUpResult.Ok)
                    returnString(args.Result);
            };
            input.Title = title??string.Empty;
            input.Message = message;
            input.IsCancelVisible = true;
            input.Show();

        }
    }
}
