using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    interface IEmployeeService
    {
        List<vwEmployee> GetAllEmployees();
        void DeleteEmployee(int employeeID);
        tblEmployee AddEditEmployee(tblEmployee employee);
    }
}
