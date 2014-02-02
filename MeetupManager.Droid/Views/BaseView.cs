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
using Google.Analytics.Tracking;
using MeetupManager.Droid.Helpers;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Droid.Views
{
    public class BaseView : MvxActionBarActivity, AbsListView.IOnScrollListener
    {


        private BaseViewModel viewModel;
        private  BaseViewModel TheViewModel
        {
            get { return viewModel ?? (viewModel = base.ViewModel as EventsViewModel); }
        }

        public string Tag = string.Empty;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
        }

        protected override void OnStart()
        {
            base.OnStart();

#if !DEBUG
            // Setup Google Analytics Easy Tracker
            var tracker = EasyTracker.GetInstance(this);
            if (tracker == null)
                return;

            tracker.ActivityStart(this);
            tracker.Set(Fields.ScreenName, Tag);
            // By default, data is dispatched from the Google Analytics SDK for Android every 30 minutes.
            // You can override this by setting the dispatch period in seconds.
            GAServiceManager.Instance.SetLocalDispatchPeriod(20);
#endif
        }

        protected void LogEvent(string category, string action, string label)
        {
#if !DEBUG
            var tracker = EasyTracker.GetInstance(this);
            if (tracker == null)
                return;

            tracker.Send(MapBuilder
                .CreateEvent(category, // Event category (required)
                    action, // Event action (required)
                    label, // Event label
                    null) // Event value
                .Build());
#endif
        }



        protected override void OnStop()
        {
            base.OnStop();
#if !DEBUG
            // May return null if EasyTracker has not yet been initialized with a property Id.
			var easyTracker = EasyTracker.GetInstance(this);
            if (easyTracker == null)
                return;

			// This screen name value will remain set on the tracker and sent with
			// hits until it is set to a new value or to null.
			easyTracker.Set (Fields.ScreenName, null);
            easyTracker.ActivityStop(this);
#endif
        }


        #region Scroll change to trigger load more.
        private readonly object Lock = new object();
        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            lock (this.Lock)
            {
                var loadMore = firstVisibleItem + visibleItemCount >= (totalItemCount - 4);

                if (loadMore && this.TheViewModel.CanLoadMore && !this.TheViewModel.IsBusy)
                {
                    this.TheViewModel.LoadMoreCommand.Execute(null);
                }
            }
        }

        public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {

        }
        #endregion
    }
}