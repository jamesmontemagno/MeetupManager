
using System;
using System.Threading.Tasks;
using MeetupManager.Portable.Interfaces;
using Xamarin.Forms;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Views;

namespace MeetupManager.Portable.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		internal readonly IMeetupService meetupService;
        internal readonly IMessageDialog messageDialog;
        internal readonly Random random;
        internal readonly IDataService dataService;
        internal readonly Page page;
        public BaseViewModel (Page page)
        {
            this.page = page;
            meetupService = DependencyService.Get<IMeetupService>();
            messageDialog = DependencyService.Get<IMessageDialog>();
            dataService = DependencyService.Get<IDataService>();
            random = new Random();
		}

		bool isBusy = false;
		public bool IsBusy
		{ 
			get { return isBusy; }
			set { 
                SetProperty(ref isBusy, value);
				if (IsBusyChanged != null)
					IsBusyChanged (isBusy);
			}
		}

        bool canLoadMore = false;
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value); }
        }

		public Action<bool> IsBusyChanged { get; set; }

        Command loadMoreCommand;

        public ICommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new Command(async () => ExecuteLoadMoreCommand())); }
        }

	    protected virtual async Task ExecuteLoadMoreCommand()
	    {
	    }

        public Action<int> FinishedFirstLoad { get; set; }

        public ICommand GoToAboutCommand
        {

            get {
                return new Command(async () => 
                    {
                        if(IsBusy)
                            return;
                        await page.Navigation.PushAsync(new AboutView());
                    });
            }

        }

        protected void SetProperty<T>(
            ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null) 
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value)) 
                return;

            backingStore = value;

            if (onChanged != null) 
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
        }
	}
}

