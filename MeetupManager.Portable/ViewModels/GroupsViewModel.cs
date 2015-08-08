
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.Models;
using Xamarin.Forms;
using MeetupManager.Portable.Views;

namespace MeetupManager.Portable.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    {
        public GroupsViewModel(Page page) : base(page)
        {
            groups = new ObservableCollection<Group>();
            
        }

        ObservableCollection<Group> groups;

        public ObservableCollection<Group> Groups
        {
            get { return groups; }
            set
            {
                SetProperty(ref groups, value);
            }
        }


        Command refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        public async Task ExecuteRefreshCommand()
        {
			if (IsBusy)
				return;

            groups.Clear();
            OnPropertyChanged("Groups");
            CanLoadMore = true;
            await ExecuteLoadMoreCommand();
        }

        

        protected override async Task ExecuteLoadMoreCommand()
        {
			if (!CanLoadMore || IsBusy)
                return;

            IsBusy = true;

            var index = Groups.Count == 0 ? 0 : Groups.Count - 1;
            try
            {
                var groupResults = await this.meetupService.GetGroups(Settings.UserId, groups.Count);
                foreach (var group in groupResults.Groups)
				{
					if(group.GroupPhoto == null)
					{
						group.GroupPhoto = new GroupPhoto{
							PhotoId = 0,
                            HighResLink = "http://refractored.com/default.png",
                            PhotoLink = "http://refractored.com/default.png",
                            ThumbLink = "http://refractored.com/default.png"
						};
					}
                    Groups.Add(group);
				}

                OnPropertyChanged("Groups");
				CanLoadMore = groupResults.Groups.Count == 100;

                if(Groups.Count == 0)
                    messageDialog.SendToast("You do not have any groups.");
            }
            catch (Exception ex)
            {
							if(Settings.Insights)
								Xamarin.Insights.Report (ex);
            }
            finally
            {

                FinishedFirstLoad?.Invoke(index);
                IsBusy = false;
            }
        }

      

		public void GoToAbout()
		{
            //TODO: Navigate
			//ShowViewModel<AboutViewModel> ();
		}

        Command<Group> goToGroupCommand;

        public ICommand GoToGroupCommand
        {
            get
            {
                return goToGroupCommand ??
                    (goToGroupCommand = new Command<Group>(async g => await ExecuteGoToGroupCommand(g)));
            }
        }

        async Task ExecuteGoToGroupCommand(Group @group)
        {
            if (IsBusy)
                return;
            
            await page.Navigation.PushAsync(new EventsView(group));
        }
    }
}
