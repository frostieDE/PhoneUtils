using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PhoneUtils.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool))
            {
                throw new ArgumentException("value must be typeof bool");
            }

            return (bool)value == true ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (!(value is Visibility))
            {
                throw new ArgumentException("value must be type of Visibility");
            }

            return (Visibility)value == Visibility.Visible ? true : false;
        }
    }
}
