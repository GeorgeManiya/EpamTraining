using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace LinguisticFeaturesAnalyzer.Data
{
    class BooleanToBrushConverter : IValueConverter
    {
        public Brush True { get; set; }
        public Brush False { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool val;
            Boolean.TryParse(value.ToString(), out val);
            return val ? True : False;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
