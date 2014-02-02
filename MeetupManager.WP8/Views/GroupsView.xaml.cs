using System;
using MeetupManager.Portable.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Views;

namespace MeetupManager.WP8.Views
{
    public partial class GroupsView : MvxPhonePage
    {
        public new GroupsViewModel ViewModel
        {
            get { return (GroupsViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public GroupsView()
        {
            InitializeComponent();
        }

        private void appBar_refresh_Click(object sender, EventArgs e)
        {
            this.ViewModel.RefreshCommand.Execute();
        }
    }
}