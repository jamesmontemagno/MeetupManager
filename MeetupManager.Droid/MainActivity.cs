
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using MeetupManager.UI;
using Android.Graphics.Drawables;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Droid;
using Android.Content.PM;
using MeetupManager.Portable.Models.Database;

namespace MeetupManager.Droid
{
    [Activity (Label = "Meetup Manager", Icon = "@drawable/ic_launcher", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);



            Forms.Init (this, bundle);


            #if DEBUG
            Xamarin.Insights.Initialize(Xamarin.Insights.DebugModeKey, this);
            #else
            Xamarin.Insights.Initialize(Xamarin.Insights.DebugModeKey, this);
            Xamarin.Insights.ForceDataTransmission = true;
            #endif
           
            #if UITEST
            Xamarin.Forms.Forms.ViewInitialized += (object sender, Xamarin.Forms.ViewInitializedEventArgs e) => {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId)) {
                e.NativeView.ContentDescription = e.View.StyleId;
                }
            };
            #endif

            var libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            MeetupManagerDatabase.Root = libraryPath;

            LoadApplication (new App ());
            ImageCircleRenderer.Init();
            //new Syncfusion.SfChart.XForms.Droid.SfChartRenderer();
            ActionBar.SetIcon ( new ColorDrawable (Resources.GetColor (Android.Resource.Color.Transparent)));
        }
    }
}

