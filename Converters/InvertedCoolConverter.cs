using System.Globalization;

namespace The_Hunt_Khai_Tan_Sum.Converters;

public class InvertedBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return !b;

        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
            return !b;

        return false;
    }
}