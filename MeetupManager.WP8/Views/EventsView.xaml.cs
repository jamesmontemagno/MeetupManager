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
    }
}