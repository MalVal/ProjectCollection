using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectCollectionClassLibrary;
using System.Windows.Data;
using System.Windows.Media;

namespace ProjectCollectionWpfApp
{
    internal class ElementToColorConverter : IValueConverter
    {
        public object Convert(object element, Type targetType, object parameter, CultureInfo culture)
        {
            if (element is Comics)
            {
                return Brushes.AliceBlue;
            }
            else if (element is Vinyl)
            {
                return Brushes.AntiqueWhite;
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
