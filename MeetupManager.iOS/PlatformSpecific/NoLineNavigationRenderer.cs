using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using MeetupManager.iOS.PlatformSpecific;
using UIKit;


[assembly: ExportRenderer(typeof(NavigationPage), typeof(NoLineNavigationRenderer))] 
namespace MeetupManager.iOS.PlatformSpecific
{
    public class NoLineNavigationRenderer : NavigationRenderer 
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // remove lower border and shadow of the navigation bar
            NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIImage ();
        }
    }
}
