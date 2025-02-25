using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MediaPlayer
{
    public class RatingToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rating)
            {
                switch (rating)
                {
                    case 1:
                        return Brushes.Crimson;
                    case 2:
                        return Brushes.Orange;
                    case 3:
                        return Brushes.Yellow;
                    case 4:
                        return Brushes.YellowGreen;
                    case 5:
                        return Brushes.Green;
                    default:
                        return Brushes.White;
                }
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
