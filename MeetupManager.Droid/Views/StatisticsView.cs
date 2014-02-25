using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using BarChart;
using Cirrious.CrossCore;
using MeetupManager.Core.Helpers;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Droid.Views
{
    [Activity(Label = "Statistics", Icon = "@drawable/ic_launcher")]
    public class StatisticsView : ActionBarActivity
    {
        private StatisticsViewModel viewModel;
        private BarChartView barChart;
        private ProgressBar progressBar;
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.view_stats);

            var name = Intent.GetStringExtra("name");
            var id = Intent.GetStringExtra("id");
            viewModel = new StatisticsViewModel(Mvx.Resolve<IMeetupService>(), Mvx.Resolve<IDataService>());
            viewModel.Init(id, name);
            SupportActionBar.Title = viewModel.GroupName;
            barChart = FindViewById<BarChartView>(Resource.Id.barChart);
            barChart.LegendColor = Color.Black;
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);

            // Perform any additional setup after loading the view, typically from a nib.
        }

        protected async override void OnStart()
        {
            base.OnStart();
            LoadData();
        }

        private async void LoadData()
        {
            RunOnUiThread(() =>
            {
                progressBar.Visibility = ViewStates.Visible;
                progressBar.Indeterminate = true;
            });
            try
            {


                var items =
                    await
                        BarHelper.GenerateData(viewModel, Resources.GetColor(Resource.Color.bar_up),
                            Resources.GetColor(Resource.Color.bar_down));
                if (items.Count == 0)
                    return;

                RunOnUiThread(() =>
                {
                    barChart.ItemsSource = items;
                    barChart.Invalidate();
                });
            }
            finally
            {
                RunOnUiThread(() =>
                {
                    progressBar.Visibility = ViewStates.Gone;
                    progressBar.Indeterminate = false;
                });
            }
        }
    }
}