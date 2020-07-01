using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Zadatak_1.Validations
{
   public class PhoneNoValidation:ValidationRule
    {
        /// <summary>
        /// Method to validate phone number input
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns>true if valid, false if its not</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string phoneNo = value as string;


            if (phoneNo.Length > 15)
                return new ValidationResult(false, "Number cannot contain more than 15 digits");

            for (int i = 0; i < phoneNo.Length; i++)
            {
                if (!Char.IsDigit(phoneNo, i))
                    return new ValidationResult(false, "Phone must contain only digits.");
            }
            return new ValidationResult(true, null);
        }
        
    }
}
