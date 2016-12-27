using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OnlineShop.Client.Validators
{
    class QuantityValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int quantity;
            Boolean noIllegalChars;
            noIllegalChars = int.TryParse(value.ToString(), out quantity);

            if (!noIllegalChars)
            {
                return new ValidationResult(false, "Illegal Characters");
            }
            else if(quantity < 1)
            {
                return new ValidationResult(false, "The quantity of products, shall be more, than 0");
            }

            return new ValidationResult(true, null);
        }
    }
}
