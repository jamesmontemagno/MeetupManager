using MeetupManager.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MeetupManager.WP8.PlatformSpecific
{
    public class WP8MeetupLogin : ILogin
    {
        public WP8MeetupLogin()
        {
        }

        public void LoginAsync(Action<bool> loginCallback)
        {
            loginCallback(true);
        }

    }
}
