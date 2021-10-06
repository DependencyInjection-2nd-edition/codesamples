using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Ploeh.Samples.ProductManagement.UWPClient.Converters
{
    public class DecimalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) =>
            ((decimal)value).ToString("F2");

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return decimal.TryParse(value.ToString(), out decimal result) ? result : DependencyProperty.UnsetValue;
        }
    }
}