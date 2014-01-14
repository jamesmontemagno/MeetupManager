using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
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
            throw new NotImplementedException();
        }

        public void SendToast(string message)
        {
            MessageBox.Show(message);
        }

        public void SendConfirmation(string message, string title, Action<bool> confirmationAction)
        {
            throw new NotImplementedException();
        }

        public void AskForString(string message, string title, Action<string> returnString)
        {
            throw new NotImplementedException();
        }
    }
}
