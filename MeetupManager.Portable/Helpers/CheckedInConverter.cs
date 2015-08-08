using System;
using Xamarin.Forms;
using System.Globalization;

namespace MeetupManager.Portable.Helpers
{
    class CheckedInConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null && (bool)value)
            {
                return ImageSource.FromFile("ic_action_toggle_check_box.png");
            }


            return ImageSource.FromFile("ic_action_toggle_check_box_outline_blank.png");
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}

