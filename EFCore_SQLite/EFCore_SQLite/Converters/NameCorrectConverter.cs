using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace EFCore_SQLite.Converters
{
    public class NameCorrectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return isNameCorrect(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        private bool isNameCorrect(object value)
        {
            if (value is string)
            {
                //bool isEmail = Regex.IsMatch(
                //    (string)value, "^[a-z0-9._-]+@[a-z0-9._-]+\\.[a-z]{2,6}$");

                int length = ((string)value).Trim().Length;

                //if (length >= 7 && length <= 60 && isEmail)
                if (length >= 8 && length <= 20)
                    return true;
                else
                    return false;

            }
            return false;
        }
    }
}
