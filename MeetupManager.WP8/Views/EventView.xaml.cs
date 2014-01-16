using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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

        private void appBar_selectwinner_Click(object sender, EventArgs e)
        {
            this.ViewModel.SelectWinnerCommand.Execute();
        }

        private void appBar_refresh_Click(object sender, EventArgs e)
        {
            this.ViewModel.RefreshCommand.Execute();
        }

        private void appBar_addmember_Click(object sender, EventArgs e)
        {
            this.ViewModel.AddNewUserCommand.Execute();
        }
    }
}