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

        /// <summary>
        /// Method for adding new employee into database or editing exsisting one
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>employee object</returns>
        public tblEmployee AddEditEmployee(tblEmployee employee)
        {
            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {
                    if(employee.EmployeeID!=0)
                    {
                        tblEmployee employeeForEdit = (from e in context.tblEmployees where e.EmployeeID == employee.EmployeeID select e).First();
                        employeeForEdit.FullName = employee.FullName;
                        employeeForEdit.DateOfBirth = employee.DateOfBirth;
                        employeeForEdit.IdentityCard = employee.IdentityCard;
                        employeeForEdit.JMBG = employee.JMBG;
                        employeeForEdit.GenderID = employee.GenderID;
                        employeeForEdit.PhoneNo = employee.PhoneNo;
                        employeeForEdit.SectorID = employee.SectorID;
                        employeeForEdit.LocationID = employee.LocationID;
                        employeeForEdit.ManagerID = employee.ManagerID;
                        context.SaveChanges();
                        return employee;
                    }
                    else
                    {
                        tblEmployee newEmployee = new tblEmployee();
                        newEmployee.FullName = employee.FullName;
                        newEmployee.DateOfBirth = employee.DateOfBirth;
                        newEmployee.IdentityCard = employee.IdentityCard;
                        newEmployee.JMBG = employee.JMBG;
                        newEmployee.GenderID = employee.GenderID;
                        newEmployee.PhoneNo = employee.PhoneNo;
                        newEmployee.SectorID = employee.SectorID;
                        newEmployee.LocationID = employee.LocationID;
                        newEmployee.ManagerID = employee.ManagerID;
                        context.SaveChanges();
                        employee.EmployeeID = newEmployee.EmployeeID;
                        return employee;
                    }

                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogEditUserToFilevwUser(user, oldUserData);                                   
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
