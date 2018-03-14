using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace DustInTheWind.MedicX.Wpf.Areas.Calendar.ViewModels
{
    public class RedLetterDayConverter : IValueConverter
    {
        static Dictionary<DateTime, string> dict = new Dictionary<DateTime, string>();

        static RedLetterDayConverter()
        {
            dict.Add(new DateTime(2009, 3, 17), "St. Patrick's Day");
            dict.Add(new DateTime(2009, 3, 20), "First day of spring");
            dict.Add(new DateTime(2009, 4, 1), "April Fools");
            dict.Add(new DateTime(2009, 4, 22), "Earth Day");
            dict.Add(new DateTime(2009, 5, 1), "May Day");
            dict.Add(new DateTime(2009, 5, 10), "Mother's Day");
            dict.Add(new DateTime(2009, 6, 21), "First Day of Summer");
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text;

            if (!dict.TryGetValue((DateTime)value, out text))
                text = null;

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}