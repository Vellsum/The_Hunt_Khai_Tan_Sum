using System.Globalization;

namespace The_Hunt_Khai_Tan_Sum.Converters;

public class CountToProgressConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int count)
        {
            // scale small counts to visible progress
            return Math.Min(count / 10.0, 1.0);
        }

        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}