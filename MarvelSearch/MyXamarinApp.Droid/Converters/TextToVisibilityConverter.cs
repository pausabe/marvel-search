using System;
using System.Globalization;
using MvvmCross.Converters;

namespace MyXamarinApp.Droid.Converters
{
    public class TextToVisibilityConverter : IMvxValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                return text == string.Empty ? "gone" : "visible";
            }

            return "gone";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}