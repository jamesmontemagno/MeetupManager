using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MeetupManager.Portable.Helpers;
using Xamarin.Forms;
using System.Collections.ObjectModel;


namespace MeetupManager.Portable.ViewModels
{
    public class DataPoint
    {
        public long Time { get; set; }
        public string Date { get; set; }
        public int CheckIns { get; set; }
        public int Guests { get; set; }
    }
    public class StatisticsViewModel : BaseViewModel
    {
        public bool ShowPopUps { get; set; }

        string groupId;

        public ObservableCollection<DataPoint> CheckInData { get; set; }

        public StatisticsViewModel(Page page,string gId, string gName) : base(page)
        {
            groupId = gId;
            GroupName = gName;
            CheckInData = new ObservableCollection<DataPoint>();
            ShowPopUps = true;
        }
            

        public string GroupName{ get; set; }


        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }

        Command refreshCommand;

        public ICommand RefreshCommand
        {
            get { return refreshCommand ?? (refreshCommand = new Command(async () => ExecuteRefreshCommand())); }
        }


        public async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                CheckInData.Clear();
			
                var dataPoints = new List<DataPoint>();
                var newMembers = await dataService.GetNewMembersForGroup(groupId);
                var rsvps = await dataService.GetRSVPsForGroup(groupId);

                foreach (var member in newMembers)
                {
                    var data = dataPoints.FirstOrDefault(d => d.Time == member.EventDate);
                    if(data == null)
                    {
                        data = new DataPoint 
                            { 
                                Time = member.EventDate, 
                                Date = FromUnixTime(member.EventDate).ToString("MM/dd/yy")
                            };
                        dataPoints.Add(data);
                    }

                    data.Guests++;

                }

                foreach (var member in rsvps)
                {
                    var data = dataPoints.FirstOrDefault(d => d.Time == member.EventDate);
                    if(data == null)
                    {
                        data = new DataPoint 
                            { 
                                Time = member.EventDate, 
                                Date = FromUnixTime(member.EventDate).ToString("MM/dd/yy")
                            };
                        dataPoints.Add(data);
                    }

                    data.CheckIns++;
                }


                foreach(var item in dataPoints.OrderBy(s => s.Time))
                    CheckInData.Add(item);

                if (ShowPopUps)
                {
                    if (CheckInData.Count == 0)
                    {
                        messageDialog
                            .SendMessage("There is no data for this group, please check-in a few members first to a meetup.",
                                "No Statistics");
                    }
                }

            }
            catch (Exception ex)
            {
                if (Settings.Insights)
                    Xamarin.Insights.Report(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}

