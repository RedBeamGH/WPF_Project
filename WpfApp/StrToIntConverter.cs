using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WpfApp
{
    public class StrToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? converted = value.ToString();
            if (converted != null)
                return converted;
            else
            {
                MessageBox.Show("Invalid input");
                return "0";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as string;
            int output;
            if (int.TryParse(input, out output))
                return output;
            else
                return DependencyProperty.UnsetValue;
        }
    }

    public class StrToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? converted = value.ToString();
            if (converted != null)
                return converted;
            else
            {
                MessageBox.Show("Invalid input");
                return "0";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as string;
            double output;
            if (double.TryParse(input, out output))
                return output;
            else
            {
                MessageBox.Show("Invalid input");
                return DependencyProperty.UnsetValue;
            }
        }
    }
}
