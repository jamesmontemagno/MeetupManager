using System.Threading.Tasks;
using System.Windows.Input;
using MeetupManager.Portable.Models.Database;
using Xamarin.Forms;

namespace MeetupManager.Portable.ViewModels
{
    public class NewUserViewModel : BaseViewModel
    {
        public NewUserViewModel(Page page) : base(page)
        {
        }
        string eventId, eventName, groupId, groupName;
        long eventDate;

        public void Init(string id, string eName, string gId, string gName, long eDate)
        {
            eventId = id;
            eventName = eName;
            groupId = gId;
            groupName = gName;
            eventDate = eDate;
        }

        string userName = string.Empty;

        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        Command saveUserCommand;

        public ICommand SaveUserCommand
        {
            get { return saveUserCommand ?? (saveUserCommand = new Command(async () => ExecuteSaveUserCommand())); }
        }

        async Task ExecuteSaveUserCommand()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                messageDialog.SendToast("Please enter a valid name to check in.");
                return;
            }
            else
            {
                await dataService.AddNewMember(new NewMember(eventId, UserName, eventName, groupId, groupName, eventDate));
                messageDialog.SendMessage(UserName + " you are all set!");
            }
        }
    }
}
