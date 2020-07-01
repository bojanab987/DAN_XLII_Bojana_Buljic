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
        vwEmployee AddEditEmployee(vwEmployee employee);
        List<tblGender> GetGenders();
        List<vwManager> GetPossibleManagers();
        vwManager GetManager(string managerName);
        bool IsManager(int employeeID);
        tblEmployee GetEmployeeJMBG(string JMBG);
        tblEmployee GetEmployeeIDCard(string identityCard);
    }
}
