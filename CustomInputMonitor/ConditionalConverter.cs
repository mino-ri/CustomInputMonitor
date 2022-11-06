using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomInputMonitor;

[ValueConversion(typeof(bool), typeof(object))]
public class ConditionalConverter : IValueConverter
{
    public object? TrueValue { get; set; }

    public object? FalseValue { get; set; }

    public object? NullValue { get; set; }

    public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value switch
        {
            bool @bool => @bool ? TrueValue : FalseValue,
            _ => NullValue,
        };
    }

    public object? ConvertBack(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
