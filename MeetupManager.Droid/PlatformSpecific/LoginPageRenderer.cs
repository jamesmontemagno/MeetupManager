using System;
using MeetupManager.Portable.Views;
using MeetupManager.Droid.PlatformSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Auth;
using MeetupManager.Portable.Services;
using Android.App;

[assembly:ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace MeetupManager.Droid.PlatformSpecific
{
    public class LoginPageRenderer : PageRenderer
    {
        LoginPage page;
        bool loginInProgress;
        public LoginPageRenderer()
        {
        }
        protected override async void OnElementChanged (ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged (e);

            if (e.OldElement != null || Element == null)
                return;


            page = e.NewElement as LoginPage;

            if (page == null || loginInProgress)
                return;

            loginInProgress = true;
            try
            {
                var auth = new OAuth2Authenticator(MeetupService.ClientId, MeetupService.ClientSecret, string.Empty, new Uri(MeetupService.AuthorizeUrl), new Uri(MeetupService.RedirectUrl), new Uri(MeetupService.AccessTokenUrl));

                auth.AllowCancel = true;
                // If authorization succeeds or is canceled, .Completed will be fired.
                auth.Completed += async (s, ee) => {
 
                        await page.Navigation.PopAsync ();
                        await page.ViewModel.FinishLogin(ee.IsAuthenticated, ee.Account == null ? null : ee.Account.Properties);
                        loginInProgress = false; 
                };

                auth.Error += async (s, ee) => 
                    {
                        //await page.Navigation.PopAsync ();  
                        //await page.ViewModel.FinishLogin(false, null);
                        //loginInProgress = false;
                    };
                var activity = Xamarin.Forms.Forms.Context as Activity;
                activity.StartActivity (auth.GetUI (Xamarin.Forms.Forms.Context));
            }
            catch(Exception ex) {
                Console.WriteLine (ex);
                await page.Navigation.PopAsync ();  
                await page.ViewModel.FinishLogin(false, null);
                loginInProgress = false;
            }


        }
    }
}

