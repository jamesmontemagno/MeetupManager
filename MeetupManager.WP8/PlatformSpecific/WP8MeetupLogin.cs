using System.Globalization;
using System.Windows.Navigation;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.WindowsPhone.Platform;
using MeetupManager.Portable.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using MeetupManager.Portable.Services;
using Microsoft.Phone.Controls;

namespace MeetupManager.WP8.PlatformSpecific
{
    public class WP8MeetupLogin : ILogin
    {
        private WebBrowser browser;
        public WebBrowser Browser
        {
            get { return browser; }
            set
            {
                browser = value;
                browser.Navigated += BrowserNavigated;
            }
        }
        private Action<bool, Dictionary<string, string>> LoginCallback { get; set; }
        public void LoginAsync(Action<bool, Dictionary<string, string>> loginCallback)
        {
            LoginCallback = loginCallback;
            var url = "https://secure.meetup.com/oauth2/authorize" +
                      "?client_id=" + MeetupService.ClientId
                      +"&response_type=code&redirect_uri=" + MeetupService.RedirectUrl;

            
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                // change UI here
                Browser.Visibility = Visibility.Visible;
                Browser.Navigate(new Uri(url));
            });
            
        }

        /// <summary>
        /// We need to check the navigation if we have a success or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BrowserNavigated(object sender, NavigationEventArgs e)
        {
            if (e.Uri.AbsoluteUri.StartsWith("http://www.refractored."))
            {
                var code = string.Empty;
                if (e.Uri.Query.Contains("code"))
                {
                    var items = e.Uri.ParseQueryString();
                    code = items["code"];
                }
                else
                {
                    LoginCallback(false, null);
                }

                Browser.Visibility = Visibility.Collapsed;
                var service = Mvx.Resolve<IMeetupService>();
                var result = await service.GetToken(code);
                if (result == null)
                {
                    LoginCallback(false, null);
                    return;
                }
                var stuff = new Dictionary<string, string>();
                stuff.Add("access_token", result.access_token);
                stuff.Add("token_type", result.token_type);
                stuff.Add("expires_in", result.expires_in.ToString(CultureInfo.InvariantCulture));
                stuff.Add("refresh_token", result.refresh_token);
                LoginCallback(true, stuff);
            }
        }
    }
}
