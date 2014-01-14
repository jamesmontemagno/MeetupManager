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
    }
}