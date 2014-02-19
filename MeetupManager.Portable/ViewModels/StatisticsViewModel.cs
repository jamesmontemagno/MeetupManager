using System;
using MeetupManager.Portable.Interfaces;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using System.Threading.Tasks;
using MeetupManager.Portable.Interfaces.Database;
using System.Collections.ObjectModel;

namespace MeetupManager.Portable.ViewModels
{
	public class StatisticsViewModel : BaseViewModel
	{
		IDataService dataService;

		private string groupId;

		public StatisticsViewModel (IMeetupService service, IDataService dataService) : base(service)
		{
			GroupsEventsCount = new Dictionary<long, int> ();

			this.dataService = dataService;
		}

		public Dictionary<long, int> GroupsEventsCount
		{
			get; set;
		}

		public void Init(string gId, string gName)
		{
			groupId = gId;
			GroupName = gName;
		}

		public string GroupName{ get; set; }


		public DateTime FromUnixTime(long unixTime)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddMilliseconds(unixTime);
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

			IsBusy = true;
			try
			{
				GroupsEventsCount.Clear ();
			

				var newMembers = await dataService.GetNewMembersForGroup (groupId);
				var rsvps = await dataService.GetRSVPsForGroup (groupId);

				foreach (var member in newMembers) {

					if (!GroupsEventsCount.ContainsKey (member.EventDate)) {
						GroupsEventsCount.Add (member.EventDate, 0);
					}

					GroupsEventsCount [member.EventDate]++;
				}
				foreach (var member in rsvps) {
					if (!GroupsEventsCount.ContainsKey (member.EventDate)) {
						GroupsEventsCount.Add (member.EventDate, 0);
					}

					GroupsEventsCount [member.EventDate]++;
				}
			}
			catch(Exception ex) {
			}
			finally{
				IsBusy = false;
			}

		}
	}
}

