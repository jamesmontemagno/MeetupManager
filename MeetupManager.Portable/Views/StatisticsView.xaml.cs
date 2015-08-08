using System;
using System.Collections.Generic;

using Xamarin.Forms;
using MeetupManager.Portable.ViewModels;
//using Syncfusion.SfChart.XForms;

namespace MeetupManager.Portable.Views
{
    public partial class StatisticsView : ContentPage
    {
        StatisticsViewModel viewModel;
        public StatisticsView(string gId, string gName)
        {
            InitializeComponent();
            BindingContext = viewModel = new StatisticsViewModel(this, gId, gName);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.ExecuteRefreshCommand();

            if (viewModel.CheckInData.Count > 0)
            {
                var max = Device.Idiom == TargetIdiom.Phone ? 7.0 : 16.0;

                var factor = max / (double)viewModel.CheckInData.Count;
                if (factor > 1.0)
                    factor = 1.0;
                else if (factor < .1)
                    factor = .1;
                
                //Chart.PrimaryAxis.ZoomFactor = factor;

            }
            else
            {
                //Chart.IsVisible = false;
            }

           
        }
    }
}

