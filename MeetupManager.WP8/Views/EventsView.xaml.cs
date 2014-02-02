using System;
using MeetupManager.Portable.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Views;

namespace MeetupManager.WP8.Views
{
    public partial class EventsView : MvxPhonePage
    {
        public new EventsViewModel ViewModel
        {
            get { return (EventsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public EventsView()
        {
            InitializeComponent();
        }

        private void AppBarRefreshClick(object sender, EventArgs e)
        {
            this.ViewModel.RefreshCommand.Execute();
        }
    }
}