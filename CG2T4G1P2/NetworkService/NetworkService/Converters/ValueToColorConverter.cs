using System.Windows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Media;

namespace NetworkService.Converters
{
    /// <summary>
    /// Sluzi da izmeni boju na kanvasu za chart ako je van opsega put 
    /// </summary>

    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double number)
            {
                return number > 5 ? Brushes.Red : Brushes.CadetBlue;
            }
            throw new InvalidOperationException("Unsupported type");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
