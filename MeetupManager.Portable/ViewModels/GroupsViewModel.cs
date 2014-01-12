/*
 * MeetupManager:
 * Copyright (C) 2013 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Collections.ObjectModel;
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
			if (IsBusy)
				return;

            groups.Clear();
            RaisePropertyChanged(() => Groups);
            CanLoadMore = true;
            await ExecuteLoadMoreCommand();
        }

        private MvxCommand loadMoreCommand;

        public IMvxCommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new MvxCommand(async () => ExecuteLoadMoreCommand())); }
        }

        private async Task ExecuteLoadMoreCommand()
        {
			if (!CanLoadMore || IsBusy)
                return;

            IsBusy = true;


            try
            {
                var groupResults = await this.meetupService.GetGroups(Settings.UserId, groups.Count);
                foreach (var group in groupResults.Groups)
				{
					if(group.GroupPhoto == null)
					{
						group.GroupPhoto = new GroupPhoto{
							PhotoId = 0,
							HighResLink = "http://img2.meetupstatic.com/9602342981488429316/img/header/logo.png",
							PhotoLink = "http://img2.meetupstatic.com/9602342981488429316/img/header/logo.png",
							ThumbLink = "http://img2.meetupstatic.com/9602342981488429316/img/header/logo.png"
						};
					}
                    Groups.Add(group);
				}

                RaisePropertyChanged(() => Groups);
				CanLoadMore = groupResults.Groups.Count == 100;
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
            ShowViewModel<EventsViewModel>(new {id = e.Id, groupName = e.Name});
        }
    }
}
