using System;
using Cirrious.MvvmCross.WindowsPhone.Views;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.WP8.Views
{
    public partial class EventView : MvxPhonePage
    {
        public new EventViewModel ViewModel
        {
            get { return (EventViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public EventView()
        {
            InitializeComponent();
        }

        private void AppBarSelectWinnerClick(object sender, EventArgs e)
        {
            this.ViewModel.SelectWinnerCommand.Execute();
        }

        private void AppBarRefreshClick(object sender, EventArgs e)
        {
            this.ViewModel.RefreshCommand.Execute();
        }

        private void AppBarAddNewUserClick(object sender, EventArgs e)
        {
            this.ViewModel.AddNewUserCommand.Execute();
        }
    }
}