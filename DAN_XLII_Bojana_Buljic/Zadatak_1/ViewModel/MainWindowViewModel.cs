using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel:ViewModelBase
    {
        MainWindow main;
        ILocationService locationService=new LocationService();
        IEmployeeService employeeService = new EmployeeService();
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        #region Constructors
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            locationService.AddLocation();
            employeeList = employeeService.GetAllEmployees();
        }
        #endregion

        #region Properties
        private List<vwEmployee> employeeList;
        public List<vwEmployee> EmployeeList
        {
            get { return employeeList; }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private vwEmployee viewEmployees;
        public vwEmployee ViewEmployees
        {
            get { return viewEmployees; }
            set
            {
                viewEmployees = value;
                OnPropertyChanged("ViewEmployees");
            }
        }

        private vwEmployee employee;
        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private bool isEmployeeDeleted;
        public bool IsEmployeeDeleted
        {
            get { return isEmployeeDeleted; }
            set { isEmployeeDeleted = value; }
        }
        #endregion

        #region Commands
        /// <summary>
        /// delete command
        /// </summary>
        private ICommand deleteEmployee;
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }

        private void DeleteEmployeeExecute()
        {
            try
            {
                if (Employee != null)
                {

                    MessageBoxResult result = MessageBox.Show("Are you sure that you want to delete this employee?\n", "Be sure!",
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    int employeeID = employee.EmployeeID;

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            employeeService.DeleteEmployee(employeeID);
                            EmployeeList = employeeService.GetAllEmployees().ToList();   
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanDeleteEmployeeExecute()
        {
            if (Employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand editEmployee;
        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditEmployeeExecute());
                }
                return editEmployee;
            }
        }

        private void EditEmployeeExecute()
        {
            try
            {
                if (Employee != null)
                {
                    EditEmployee editEmployee = new EditEmployee(Employee);
                    editEmployee.ShowDialog();

                    //we read employees from database in case we added new 
                    EmployeeList = employeeService.GetAllEmployees().ToList();                  

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditEmployeeExecute()
        {
            if (Employee == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand addEmployee;
        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddEmployeeExecute(), param => CanAddEmployeeExecute());
                }
                return addEmployee;
            }
        }

        private void AddEmployeeExecute()
        {
            try
            {
                AddEmployee addEmployee= new AddEmployee();
                addEmployee.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddEmployeeExecute()
        {
            return true;
        }
        #endregion
    }
}
