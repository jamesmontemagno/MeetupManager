using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.ViewModel.RefreshCommand.Execute();
        }

        private void appBar_refresh_Click(object sender, EventArgs e)
        {
            this.ViewModel.RefreshCommand.Execute();
        }
    }
}