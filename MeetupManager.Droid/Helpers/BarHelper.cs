using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Graphics;
using BarChart;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Droid.Views;
using MeetupManager.Portable.Interfaces;
using MeetupManager.Portable.ViewModels;

namespace MeetupManager.Core.Helpers
{
    public static class BarHelper
    {
#if __ANDROID__
        public static async Task<List<BarModel>> GenerateData(StatisticsViewModel vm, Color up, Color down)
#elif __IOS__
        public static async Task<List<BarModel>> GenerateData(StatisticsViewModel vm, UIColor up, UIColor down)
#endif
        {
            var models = new List<BarModel>();

            if (vm.GroupsEventsCount.Count == 0)
                await vm.ExecuteRefreshCommand();

            int previous = -1;
            foreach (var eventInGroup in vm.GroupsEventsCount)
            {
                var color = previous == -1 || previous < eventInGroup.Value ? up : down;
                models.Add(new BarModel() { Value = eventInGroup.Value, Color = color, Legend = vm.FromUnixTime(eventInGroup.Key).ToString("MM/dd/yy") });
                previous = eventInGroup.Value;
            }

            
            return models;
        }
    }
}