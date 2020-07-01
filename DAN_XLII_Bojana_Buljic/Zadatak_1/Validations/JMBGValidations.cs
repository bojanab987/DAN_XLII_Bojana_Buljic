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
    public class JMBGValidations:ValidationRule
    {
        IEmployeeService service = new EmployeeService();
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string JMBG = value as string;
            if(JMBG.Length != 13)
            {
                return new ValidationResult(false,"JMBG should contain 13 digits");
            }
            else
            {
                for(int i=0; i<13; i++)
                {
                    if(!int.TryParse(JMBG[i].ToString(), out int digit))
                    {
                        return new ValidationResult(false, "JMBG should contain 13 digits");
                    }
                }
                
            }
            tblEmployee employee = service.GetEmployeeJMBG(JMBG);
            if (employee != null)
            {
                return new ValidationResult(false, "JMBG already exists in database.");
            }
            

            DateTime now = DateTime.Now;
            int year = 0;

            if (Char.GetNumericValue(JMBG[4]) == 0)
            {
                year = 2000 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);
                if (year > now.Year)
                {
                    return new ValidationResult(false, "JMBG is not valid year");
                }
            }
            else if (Char.GetNumericValue(JMBG[4]) == 9)
            {
                year = 1900 + 10 * (int)Char.GetNumericValue(JMBG[5]) + (int)Char.GetNumericValue(JMBG[6]);
            }
            else
            {
                return new ValidationResult(false, "JMBG is not valid year");
            }

            int month = (int)Char.GetNumericValue(JMBG[2]) * 10 + (int)Char.GetNumericValue(JMBG[3]);

            if (year == now.Year && month > now.Month)
            {
                return new ValidationResult(false, "JMBG is not valid month");
            }

            if (month == 0 || month > 12)
            {
                return new ValidationResult(false, "JMBG is not valid month");
            }

            int day = (int)Char.GetNumericValue(JMBG[0]) * 10 + (int)Char.GetNumericValue(JMBG[1]);

            if (year == now.Year && month == now.Month && day >= now.Day)
            {
                return new ValidationResult(false, "JMBG is not valid day");
            }

            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                if (day == 0 || day > 31)
                {
                    return new ValidationResult(false, "JMBG is not valid day");
                }

            }
            else if (month == 4 || month == 6 || month == 9 || month == 11)
            {
                if (day == 0 || day > 30)
                {
                    return new ValidationResult(false, "JMBG is not valid day");
                }
            }
            else if (month == 2)
            {
                if (DateTime.IsLeapYear(year))
                {
                    if (day == 0 || day > 29)
                    {
                        return new ValidationResult(false, "JMBG is not valid day");
                    }
                }
                else
                {
                    if (day == 0 || day > 28)
                    {
                        return new ValidationResult(false, "JMBG is not valid day");
                    }
                }
            }
            return new ValidationResult(true, null);
        }       
        
    }
}
