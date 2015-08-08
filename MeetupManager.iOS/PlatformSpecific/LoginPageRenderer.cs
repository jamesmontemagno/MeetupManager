using System;
using Xamarin.Forms;
using MeetupManager.Portable.Views;
using Xamarin.Forms.Platform.iOS;
using MeetupManager.iOS.PlatformSpecific;
using Xamarin.Auth;
using MeetupManager.Portable.Services;
using UIKit;

[assembly:ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace MeetupManager.iOS.PlatformSpecific
{
    public class LoginPageRenderer : PageRenderer
    {
        LoginPage page;
        public LoginPageRenderer()
        {
        }


        protected override void OnElementChanged (VisualElementChangedEventArgs e)
        {
            base.OnElementChanged (e);

            if (e.OldElement != null || Element == null)
                return;

            page = e.NewElement as LoginPage;

        }


        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            if (page == null)
                return;

            this.View.BackgroundColor = UIColor.FromRGBA (0, 0, 0, 0);
        }
        bool loginInProgress;


        public override async void ViewDidAppear (bool animated)
        {
            base.ViewDidAppear (animated);


            if (page == null || loginInProgress)
                return;

            loginInProgress = true;
            try
            {
                var auth = new OAuth2Authenticator(MeetupService.ClientId, MeetupService.ClientSecret, string.Empty, new Uri(MeetupService.AuthorizeUrl), new Uri(MeetupService.RedirectUrl), new Uri(MeetupService.AccessTokenUrl));

                auth.AllowCancel = true;
                auth.ShowUIErrors = false;
                // If authorization succeeds or is canceled, .Completed will be fired.
                auth.Completed += async (s, ee) => {
                    await DismissViewControllerAsync (true);
                    await page.Navigation.PopModalAsync ();
                    await page.ViewModel.FinishLogin(ee.IsAuthenticated, ee.Account == null ? null : ee.Account.Properties);
                    loginInProgress = false; 
                };

                auth.Error += async (sender, e) => 
                    {
                        //await DismissViewControllerAsync (true);
                        //await page.Navigation.PopModalAsync ();  
                        //await page.ViewModel.FinishLogin(false, null);
                        //loginInProgress = false;
                    };

                await PresentViewControllerAsync (auth.GetUI (), true);

            }
            catch(Exception ex) {
                Console.WriteLine (ex);
                await page.Navigation.PopModalAsync ();  
                loginInProgress = false;
            }

        }
    }
}

