
using System.Threading.Tasks;
using System.Windows.Input;
using MeetupManager.Portable.Models;
using MeetupManager.Portable.Models.Database;
using Xamarin.Forms;

namespace MeetupManager.Portable.ViewModels
{

    public class MemberViewModel : BaseViewModel
    {
        public MemberViewModel(Page page) : base(page)
        {
        }
        public static string DefaultIcon = @"http://refractored.com/default.png";

        public Member Member { get; set; }

        bool checkedIn;

        public bool CheckedIn
        {
            get { return checkedIn; }
            set
            {
                SetProperty(ref checkedIn, value);
            }
        }

        private string guests;
        public string Guests
        {
            get { return guests; }
        }

        public bool CanDelete
        {
            get { return NewUserId != 0; }
        }

        public int NewUserId { get; set; }
        public bool HasGuests { get { return !string.IsNullOrWhiteSpace(guests); } }

        public MemberPhoto Photo { get; set; }
        public string ThumbLink 
        {
            get 
            { 
                if (Photo == null)
                    return DefaultIcon;
                
                return Photo.ThumbLink;
            }
        }
        readonly string eventId, eventName, groupId, groupName;
        readonly long eventDate;

        public MemberViewModel(Page page, Member member, MemberPhoto photo, string eventId, string eName, string gId, string gName, long eDate, int guests = 0) : base(page)
        {
            this.guests = string.Empty;

            if (guests == 1)
                this.guests = "1 guest";
            else if (guests > 1)
                this.guests = guests + " guests";
                
            Member = member;
            this.eventId = eventId;
            eventName = eName;
            groupId = gId;
            groupName = gName;
            eventDate = eDate;
            Photo = photo ?? new MemberPhoto
            {
                HighResLink = DefaultIcon,
                PhotoId = 0,
                ThumbLink = DefaultIcon,
                PhotoLink = DefaultIcon
            };

            if (string.IsNullOrWhiteSpace(Photo.HighResLink))
                Photo.HighResLink = DefaultIcon;


            if (string.IsNullOrWhiteSpace(Photo.ThumbLink))
                Photo.ThumbLink = DefaultIcon;

            if (string.IsNullOrWhiteSpace(Photo.PhotoLink))
                Photo.PhotoLink = DefaultIcon;
        }

        public string Name { get { return Member.Name; } }

        Command checkInCommand;

        public ICommand CheckInCommand
        {
            get { return checkInCommand ?? (checkInCommand = new Command(async () => await ExecuteCheckInCommand())); }
        }

        async Task ExecuteCheckInCommand()
        {

            await dataService.CheckInMember(new EventRSVP(eventId, Member.MemberId.ToString(), eventName, groupId, groupName, eventDate));

            CheckedIn = true;

        }
    }
}

