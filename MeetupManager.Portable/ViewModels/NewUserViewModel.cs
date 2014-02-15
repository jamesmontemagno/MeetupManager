using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.Interfaces.Database;
using MeetupManager.Portable.Models;
using MeetupManager.Portable.Models.Database;

namespace MeetupManager.Portable.ViewModels
{
    public class NewUserViewModel : MvxViewModel
    {
        private string eventId, eventName, groupId, groupName;
      private long eventDate;
        private IDataService dataService;
        public NewUserViewModel(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public void Init(string id, string eName, string gId, string gName, long eDate)
        {
          this.eventId = id;
            this.eventName = eName;
          this.groupId = gId;
          this.groupName = gName;
          this.eventDate = eDate;
        }

        private string userName = string.Empty;
        public string UserName
        {
            get { return userName; }
            set { userName = value; RaisePropertyChanged(()=>UserName); }
        }

        private IMvxCommand saveUserCommand;
        public IMvxCommand SaveUserCommand
        {
            get { return saveUserCommand ?? (saveUserCommand = new MvxCommand(async () => ExecuteSaveUserCommand())); }
        }

        private async Task ExecuteSaveUserCommand()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                Mvx.Resolve<IMessageDialog>().SendToast("Please enter a valid name to check in.");
                return;
            }
            else
            {
                await dataService.AddNewMember(new NewMember(eventId, UserName, eventName, groupId, groupName, eventDate));
                Mvx.Resolve<IMessageDialog>().SendMessage(UserName + " you are all set!");
            }
        }
    }
}
