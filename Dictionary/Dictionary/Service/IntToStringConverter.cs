using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Dictionary.Service
{
    public class IntToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Asigură-te că sunt furnizate exact două valori
            if (values.Length != 2)
                return DependencyProperty.UnsetValue;

            // Formatează textul utilizând primul și al doilea parametru
            return string.Format("{0} / {1}", values[0], values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
