using Cirrious.MvvmCross.ViewModels;
using MeetupManager.Portable.Interfaces;

namespace MeetupManager.Portable.ViewModels
{
    public class FirstViewModel 
		: BaseViewModel
    {
		public FirstViewModel(IMeetupService meetupService) : base(meetupService)
		{
		}
    }
}
