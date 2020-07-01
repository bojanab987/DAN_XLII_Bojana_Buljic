using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        ILocationService locationService;
        IEmployeeService employeeService;
        //backgroundWorker object
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        

        #region Constructors
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            locationService = new LocationService();
            employeeService = new EmployeeService();
            locationService.AddLocation();
            EmployeeList = employeeService.GetAllEmployees();
            backgroundWorker.DoWork += DeleteDoWork;
        }
        #endregion

        public void DeleteDoWork(object sender, DoWorkEventArgs e)
        {
            string result = "New employee: " + Employee.FullName + "with ID " + Employee.EmployeeID + " is deleted from database.";
            LogActions.GetLog().PrintInFile(result);
        }

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

        /// <summary>
        /// Deleting employee execution method
        /// </summary>
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
                            backgroundWorker.RunWorkerAsync();
                            IsEmployeeDeleted = true;
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

        /// <summary>
        /// Method check if deletion is possible
        /// </summary>
        /// <returns>true or false</returns>
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

        /// <summary>
        /// Edit employee command
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        private void EditEmployeeExecute()
        {
            try
            {
                if (Employee != null)
                {
                    EditEmployee editEmployee = new EditEmployee();
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

        /// <summary>
        /// Add employee command
        /// </summary>
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

        /// <summary>
        /// Method called on AddEmployee button click...adds new employee into database and updates Employee view
        /// </summary>
        private void AddEmployeeExecute()
        {
            try
            {
                AddEmployee addEmployee= new AddEmployee();
                addEmployee.ShowDialog();

                if((addEmployee.DataContext as AddEmployeeViewModel).IsUpdateEmployee==true)
                {
                    EmployeeList = employeeService.GetAllEmployees().ToList();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to check if execution of add command is possible
        /// </summary>
        /// <returns></returns>
        private bool CanAddEmployeeExecute()
        {
            return true;
        }
        #endregion
    }
}
