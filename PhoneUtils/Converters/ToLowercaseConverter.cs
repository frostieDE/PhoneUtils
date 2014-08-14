using System;
using Windows.UI.Xaml.Data;

namespace PhoneUtils.Converters
{
    public class ToLowercaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString().ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
