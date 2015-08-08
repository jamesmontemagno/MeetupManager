using System;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using MeetupManager.UI;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MeetupManager.Portable.Models.Database;
using System.IO;

namespace MeetupManager.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            MeetupManagerDatabase.Root = docsPath;
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(240, 40, 34); //bar background
            UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items
            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
                {
                    Font = UIFont.FromName("HelveticaNeue-Light", (nfloat)18f),
                    TextColor = UIColor.White
                });

            var dictionary = NSDictionary.FromObjectsAndKeys(new []{ "Mozilla/5.0 (iPhone; CPU iPhone OS 7_1 like Mac OS X) AppleWebKit/537.51.2 (KHTML, like Gecko) Version/7.0 Mobile/11D167 Safari/9537.53" }, new [] { "UserAgent" });
            NSUserDefaults.StandardUserDefaults.RegisterDefaults(dictionary);

            Forms.Init(); 
            #if DEBUG
            Xamarin.Insights.Initialize(Xamarin.Insights.DebugModeKey);
            #else
            Xamarin.Insights.Initialize(Xamarin.Insights.DebugModeKey);
            Xamarin.Insights.ForceDataTransmission = true;
            #endif

            #if UITEST
            // requires Xamarin Test Cloud Agent

            Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) => {
                // http://developer.xamarin.com/recipes/testcloud/set-accessibilityidentifier-ios/
                if (null != e.View.StyleId) {
                e.NativeView.AccessibilityIdentifier = e.View.StyleId;
                }
            };
            #endif
            ImageCircleRenderer.Init();
            //new Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
		}
	}
}

