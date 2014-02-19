using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using BarChart;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeetupManager.Portable.ViewModels;
using GCDiscreetNotification;

namespace MeetupManager.iOS.Views
{
	public partial class StatisticsView : BaseViewController
	{
		private StatisticsViewModel viewModel;
		public StatisticsViewModel VM {get { return viewModel ?? (viewModel = base.ViewModel as StatisticsViewModel);}}

		const float BarChartTopMargin = 5f;
		const float BarChartBottomMargin = 50f;
		const float BarChartHorizontalMargin = 30f;
		GCDiscreetNotificationView notificationView;


		public StatisticsView () : base ("StatisticsView", null)
		{

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();


		
			
			// Release any cached data, images, etc that aren't in use.
		}
		BarChartView barChart;
		public async override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.Title = VM.GroupName;
			EdgesForExtendedLayout = UIRectEdge.None;
			notificationView= new GCDiscreetNotificationView (
				text: "Loading Stats...",
				activity: true,
				presentationMode: GCDNPresentationMode.Bottom,
				view: View
			);

			VM.IsBusyChanged = (busy) => {
				if(busy)
					notificationView.Show (animated: true);
				else
					notificationView.HideAnimated();
			};

			barChart = new BarChartView ();
			barChart.BarOffset = 20f;
			barChart.BarWidth = 45f;
			barChart.MinimumValue = 0;
			barChart.BarCaptionInnerColor = UIColor.Black;
			barChart.BarCaptionInnerShadowColor = UIColor.White;
			barChart.BarCaptionOuterColor = UIColor.Black;
			barChart.BarCaptionOuterShadowColor = UIColor.White;

			barChart.Frame = View.Frame;

			View.AddSubview (barChart);

			barChart.ItemsSource = await GenerateData ();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public async Task<List<BarModel>> GenerateData ()
		{
			var models = new List<BarModel> ();

			await VM.ExecuteRefreshCommand ();

			int previous = -1;
			var up = UIColor.FromRGB (119, 208, 101);
			var down = UIColor.FromRGB (180, 85, 182);
			foreach (var eventInGroup in VM.GroupsEventsCount) {
				var color = previous == -1 || previous < eventInGroup.Value ? up : down;
				models.Add (new BarModel () { Value = eventInGroup.Value, Color = color, Legend = VM.FromUnixTime (eventInGroup.Key).ToString("MM/dd/yy")});
				previous = eventInGroup.Value;
			}

			return models;
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			barChart.Frame = new RectangleF (BarChartHorizontalMargin, BarChartTopMargin, View.Bounds.Width - 2 * BarChartHorizontalMargin,
				View.Bounds.Height - BarChartTopMargin - BarChartBottomMargin);
		}
	}
}

