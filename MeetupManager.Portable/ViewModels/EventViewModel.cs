using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetupManager.Portable.Helpers;
using MeetupManager.Portable.Models;
using MeetupManager.Portable.Models.Database;
using Xamarin.Forms;

namespace MeetupManager.Portable.ViewModels
{
    public class EventViewModel : BaseViewModel
    {
        string eventId;
        string groupId;

        public string GroupName { get; set; }

        public long EventDate { get; set; }

        public long Duration { get; set; }

        public string Location { get; set; }

        public EventViewModel(Page page, Event e, string gId, string gName) : base(page)
        {
            members = new ObservableCollection<MemberViewModel>();
            eventId = e.Id;
            EventName = e.Name;
            EventDate = e.Time;
            groupId = gId;
            GroupName = gName;
        }



        string eventName = string.Empty;

        public string EventName
        {
            get { return eventName; }
            set
            {
                SetProperty(ref eventName, value);
            }
        }

        string rsvpCount;

        public string RSVPCount
        {
            get { return rsvpCount; }
            set { SetProperty(ref rsvpCount, value); }
        }


        ObservableCollection<MemberViewModel> members;

        public ObservableCollection<MemberViewModel> Members
        {
            get { return members; }
            set { SetProperty(ref members, value); }
        }

        Command refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
        }

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            members.Clear();
            OnPropertyChanged("Members");
            CanLoadMore = true;
            await ExecuteLoadMoreCommand();

        }

        protected override async Task ExecuteLoadMoreCommand()
        {
            if (!CanLoadMore || IsBusy)
                return;

            //Go to database and check this user in.
            IsBusy = true;

            var index = Members.Count == 0 ? 0 : Members.Count - 1;
            try
            {
                var addNewMembers = Members.Count == 0;
                var eventResults = await meetupService.GetRSVPs(eventId, members.Count);
                foreach (var e in eventResults.RSVPs)
                {
                    var member = new MemberViewModel(page, e.Member, e.MemberPhoto, eventId, eventName, groupId, GroupName, EventDate, e.Guests);

                    member.CheckedIn = await dataService.IsCheckedIn(eventId, member.Member.MemberId.ToString(), eventName, groupId, GroupName, EventDate);

                    members.Add(member);
                }

                CanLoadMore = eventResults.RSVPs.Count == 100;

                if (addNewMembers)
                {
                    var newMembers = await dataService.GetNewMembers(eventId);
                    foreach (var e in newMembers)
                    {
                        if (string.IsNullOrEmpty(e.GroupId))
                        {
                            e.GroupId = groupId;
                            e.GroupName = GroupName;
                            e.EventName = eventName;
                            e.EventDate = EventDate;
                            e.EventId = eventId;
                        }
                        var member = new MemberViewModel(page, new Member { MemberId = -1, Name = e.Name },
                             null, eventId, eventName, groupId, GroupName, EventDate);
                        member.NewUserId = e.Id;
                        member.CheckedIn = true;
                        members.Add(member);
                    }
                }



                RefreshCount();

                if (members.Count == 0)
                    messageDialog.SendToast("No one has checked in yet.");

            }
            catch (Exception ex)
            {
                if (Settings.Insights)
                    Xamarin.Insights.Report(ex);
                CanLoadMore = false;
                messageDialog.SendToast("Unable to get RSVPs please refresh or log in again.");
            }
            finally
            {
                FinishedFirstLoad?.Invoke(index);
                IsBusy = false;
            }
        }

        Command<MemberViewModel> checkInCommand;

        public ICommand CheckInCommand
        {
            get { return checkInCommand ?? (checkInCommand = new Command<MemberViewModel>(async ev => await ExecuteCheckInCommand(ev))); }
        }

        async Task ExecuteCheckInCommand(MemberViewModel member)
        {
            if (member.Member.MemberId == -1)
            {
                messageDialog.SendToast("Only new members can be removed.");
                return;
            }
            if (member.CheckedIn)
                await dataService.CheckOutMember(eventId, member.Member.MemberId.ToString());
            else
                await dataService.CheckInMember(new EventRSVP(eventId, member.Member.MemberId.ToString(), eventName, groupId, GroupName, EventDate));

            member.CheckedIn = !member.CheckedIn;



            RefreshCount();

            if (member.CheckedIn && !string.IsNullOrWhiteSpace(member.Guests))
            {
                messageDialog.SendConfirmation("Would you like to check in your guests now? You can always check them in later by hitting the add button above.", 
                    "Check-in guests", 
                    (yes) =>
                    {
                        if(yes)
                            ExecuteAddNewUserCommand();
                    });
            }
        }

        Command selectWinner;

        public ICommand SelectWinnerCommand
        {
            get { return selectWinner ?? (selectWinner = new Command(ExecuteSelectWinnerCommand)); }
        }

        void ExecuteSelectWinnerCommand()
        {
            var potential = members.Where(m => m.CheckedIn).ToList();
            var count = potential.Count;
            string message;
            if (count == 0)
            {
                message = "No one has checked in.";
            }
            else if (count == 1)
            {
                message = potential[0].Name + " | " + potential[0].Member.MemberId;
            }
            else
            {
                var member = potential[random.Next(count)];
                message = member.Name + " | " + member.Member.MemberId;
            }

            messageDialog.SendMessage(message, "Winner!!!");
        }


        Command addNewUserCommand;

        public ICommand AddNewUserCommand
        {
            get { return addNewUserCommand ?? (addNewUserCommand = new Command(ExecuteAddNewUserCommand)); }
        }

        void ExecuteAddNewUserCommand()
        {
            messageDialog.AskForString("Please enter name:", "New Member", async name => await ExecuteSaveUserCommand(name));
        }

        Command<string> saveUserCommand;

        public ICommand SaveUserCommand
        {
            get { return saveUserCommand ?? (saveUserCommand = new Command<string>(async name => await ExecuteSaveUserCommand(name))); }
        }

        async Task ExecuteSaveUserCommand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                messageDialog.SendToast("Please enter a valid name to check-in.");
            }
            else
            {
                var newMember = new NewMember(eventId, name, eventName, groupId, GroupName, EventDate);
                await dataService.AddNewMember(newMember);
                var member = new MemberViewModel(page, new Member { MemberId = -1, Name = name },
                         null, eventId, eventName, groupId, GroupName, EventDate);

                member.CheckedIn = true;
                member.NewUserId = newMember.Id;
                Members.Add(member);
                RefreshCount();
                messageDialog.SendMessage(name + " you are all set!");
            }
        }

        Command<MemberViewModel> deleteUserCommand;

        public ICommand DeleteUserCommand
        {
            get { return deleteUserCommand ?? (deleteUserCommand = new Command<MemberViewModel>(ExecuteDeleteUserCommand)); }
        }

        void ExecuteDeleteUserCommand(MemberViewModel member)
        {
            if (member.NewUserId == 0)
            {
                messageDialog.SendToast("RSVPs can't be removed.");
                return;
            }
            var id = member.NewUserId;
            var index = Members.FirstOrDefault(m => m.NewUserId == id);
            if (index == null)
                return;

            messageDialog.SendConfirmation("Are you sure you want to remove: " + member.Name, "Remove?",
                confirmation =>
                {
                    if (!confirmation)
                        return;

                    dataService.RemoveNewMember(id);
                    Device.BeginInvokeOnMainThread(() =>
                        {
                            Members.Remove(member);
                            RefreshCount();
                            Members = members;
                        });
                });

        }

        void RefreshCount()
        {
            RSVPCount = members.Where(m => m.CheckedIn).ToList().Count + "/" + members.Count;
        }

        #region IRemove implementation

        public bool CanRemove(int index)
        {
            if (index < 0 || index > Members.Count - 1)
                return false;

            return Members[index].NewUserId != 0;
        }


        Command<int> deleteUserIndexCommand;

        public ICommand DeleteUserIndexCommand
        {
            get
            {
                return deleteUserIndexCommand ??
                (deleteUserIndexCommand = new Command<int>(index =>
                    ExecuteDeleteUserCommand(Members[index])));
            }
        }


        #endregion
    }
}

