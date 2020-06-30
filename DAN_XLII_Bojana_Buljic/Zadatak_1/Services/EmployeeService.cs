using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Method gets all employees from database and returns list of employees
        /// </summary>
        /// <returns>List of employees</returns>
        public List<vwEmployee> GetAllEmployees()
        {
            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    employees = (from x in context.vwEmployees select x).ToList();
                    return employees;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }            
        }

        /// <summary>
        /// Method for deleting employee from tblEmployees in database and logs action in file
        /// </summary>
        /// <param name="employeeID"></param>
        public void DeleteEmployee(int employeeID)
        {
            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {
                    tblEmployee employeeToDelete = (from e in context.tblEmployees where e.EmployeeID == employeeID select e).First();
                    context.tblEmployees.Remove(employeeToDelete);
                    context.SaveChanges();

                    //file log background worker
                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogDeleteUserToFile(userToDelete);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public vwEmployee EditEmployee(vwEmployee employee)
        {
            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {
                    tblEmployee employeeForEdit = (from e in context.tblEmployees where e.EmployeeID == employee.EmployeeID select e).First();
                    //tblEmployee emp = new tblEmployee();
                    //emp.FullName = employeeForEdit.FullName;
                    //employeeForEdit.FullName = employee.FullName;
                    //employeeForEdit.DateOfBirth = employee.DateOfBirth;
                    //employeeForEdit.IdentityCard = employee.IdentityCard;
                    //employeeForEdit.JMBG = employee.JMBG;
                    //employeeForEdit.GenderID = employee.Gender;
                    //employeeForEdit.PhoneNo = employee.PhoneNo;
                    //employeeForEdit.SectorID = employee.SectorName;
                    //employeeForEdit.LocationID = employee.Location;
                    //employeeForEdit.ManagerID = employee.Manager;
                    //context.SaveChanges();

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogEditUserToFilevwUser(user, oldUserData);
                    return employee;                  

                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
