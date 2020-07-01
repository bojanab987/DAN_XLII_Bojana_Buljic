using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Zadatak_1.Services;

namespace Zadatak_1.Validations
{
    public class IDCardValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            if (number.Length != 9)
            {
                return new ValidationResult(false, "Invalid id card input. ID need to have 9 digits.");
            }
            else
            {
                for (int i = 0; i < 9; i++)
                {
                    if (!int.TryParse(number[i].ToString(), out int digit))
                    {
                        return new ValidationResult(false, "JMBG should contain 9 digits");
                    }                    
                }
                return new ValidationResult(true, null);
            }
        }
       
    }
}
