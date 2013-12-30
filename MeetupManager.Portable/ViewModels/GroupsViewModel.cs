using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.Models;

namespace MeetupManager.Portable.ViewModels
{
    public class GroupsViewModel : BaseViewModel
    {
        public GroupsViewModel(IMeetupService meetupService) : base(meetupService)
        {
            groups = new ObservableCollection<Group>();
            ExecuteRefreshCommand();
        }



        private ObservableCollection<Group> groups;

        public ObservableCollection<Group> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                RaisePropertyChanged(() => Groups);
            }
        }

        private IMvxCommand refreshCommand;

        public IMvxCommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new MvxCommand(async () => ExecuteRefreshCommand())); }
        }

        public async Task ExecuteRefreshCommand()
        {
            groups.Clear();
            RaisePropertyChanged(() => Groups);
            await ExecuteLoadMoreCommand();
        }

        private MvxCommand loadMoreCommand;

        public IMvxCommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand(async () => ExecuteLoadMoreCommand())); }
        }

        private async Task ExecuteLoadMoreCommand()
        {
            IsBusy = true;


            try
            {
                var groupResults = await this.meetupService.GetGroups(Settings.UserId, groups.Count);
                foreach (var group in groupResults.Groups)
                    Groups.Add(group);

                RaisePropertyChanged(() => Groups);

            }
            catch (Exception ex)
            {
                Mvx.Resolve<IMvxTrace>().Trace(MvxTraceLevel.Error, "GroupsViewModel", ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        private MvxCommand<Group> goToGroupCommand;

        public IMvxCommand GoToGroupCommand
        {
            get
            {
                return goToGroupCommand ??
                       (goToGroupCommand = new MvxCommand<Group>(ExecuteGoToGroupCommand));
            }
        }

        private void ExecuteGoToGroupCommand(Group e)
        {
            ShowViewModel<EventsViewModel>(new {id = e.Id});
        }
    }
}
