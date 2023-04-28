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
    public class DirConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "0;0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string s = value.ToString();
                string[] arr = s.Split(";");
                double[] ans = new double[2] { double.Parse(arr[0]), double.Parse(arr[1])};
                return ans;
            }
            catch { }
            return DependencyProperty.UnsetValue;
        }
    }
}
