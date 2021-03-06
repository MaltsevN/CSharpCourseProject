﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace OnlineShop.Client.Converters
{
    [ValueConversion(typeof(DateTime), typeof(DateTime))]
    class UtcToLocalDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToLocalTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToUniversalTime();
        }
    }
}
