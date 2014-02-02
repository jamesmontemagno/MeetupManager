using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Navigation;
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
        public WP8MeetupLogin()
        {
        }

     
        public WebBrowser Browser { get; set; }
        private Action<bool, Dictionary<string, string>> LoginCallback;
        public void LoginAsync(Action<bool, Dictionary<string, string>> loginCallback)
        {
            LoginCallback = loginCallback;
            var url = "https://secure.meetup.com/oauth2/authorize" +
                      "?client_id=" + MeetupService.ClientId
                      +"&response_type=code&redirect_uri=" + redirect;


            Browser.Navigated += BrowserNavigated;
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                // change UI here
                Browser.Visibility = System.Windows.Visibility.Visible;
                Browser.Navigate(new Uri(url));
            });
            
        }

        private string url;

        private int AUTH = 0;
        private int TOKEN = 1;
        private int TOKEN2 = 2;
        private int count = 0;
        private int state;
        private string code;
        private string redirect = "http://www.refractored.com/login_success.html";

        void GetResponseStreamCallback(IAsyncResult callbackResult)
        {
            if (state == TOKEN)
            {
                HttpWebRequest request = (HttpWebRequest)callbackResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(callbackResult);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("Ok1");
                }
                else
                {
                    Debug.WriteLine(response.StatusCode);
                }
                using (StreamReader httpWebStreamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = httpWebStreamReader.ReadToEnd();
                    //For debug: show results
                    Debug.WriteLine(result);
                    var stuff = new Dictionary<string, string>();

                    var theStuff = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result);

                    stuff.Add("access_token", theStuff.access_token);
                    stuff.Add("token_type", theStuff.token_type);
                    stuff.Add("expires_in", theStuff.expires_in.ToString());
                    stuff.Add("refresh_token", theStuff.refresh_token);
                    LoginCallback(true, stuff);

                }
            }

        }


        public class RootObject
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }


        private void BrowserNavigated(object sender, NavigationEventArgs e)
        {

            if (strCmp(e.Uri.AbsoluteUri, "http://www.refractored.") == true)
            {
                if (e.Uri.Query.Contains("code"))
                {
                    var items = e.Uri.ParseQueryString();
                    code = items["code"];
                }
                var post = "https://secure.meetup.com/oauth2/access" +
                           "client_id=" + MeetupService.ClientId +
                           "&client_secret=" + MeetupService.ClientSecret +
                           "&grant_type=authorization_code" +
                           "&redirect_uri=" + redirect +
                           "&code=" + code;

                Browser.Visibility = System.Windows.Visibility.Collapsed;
                HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(post);
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                state = TOKEN;
                myRequest.BeginGetResponse(new AsyncCallback(GetResponseStreamCallback), myRequest);
                
            }

        }
        private bool strCmp(string a, string b)
        {
            if (a.Length < b.Length)
                return false;
            bool equal = false;
            for (int i = 0; i < b.Length; i++)
            {

                if (a[i] == b[i])
                    equal = true;
                else
                {
                    equal = false;
                    break;
                }

            }

            return equal;
        }
    }
}
