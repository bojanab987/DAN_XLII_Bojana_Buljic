using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Services;
using Zadatak_1.View;
using Zadatak_1.Validations;
using System.ComponentModel;

namespace Zadatak_1.ViewModel
{
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployee addEmployee;
        ILocationService locationService;
        IEmployeeService employeeService;
        ISectorService sectorService;
        BackgroundWorker bw = new BackgroundWorker();        

        #region Constructors
        public AddEmployeeViewModel(AddEmployee addEmployeeOpen)
        {
            addEmployee = addEmployeeOpen;
            
            employee = new vwEmployee();

            locationService = new LocationService();
            employeeService = new EmployeeService();
            sectorService = new SectorService();

            GenderList = employeeService.GetGenders();
            LocationList = locationService.GetAllLocations().ToList();
            LocationList.OrderBy(x => x.Location);
            Managers = employeeService.GetPossibleManagers();
            bw.DoWork += AddDoWork;
            
        }
        #endregion

        public void AddDoWork(object sender, DoWorkEventArgs e)
        {
            string result = "New employee: " + Employee.FullName + "with ID " + Employee.EmployeeID + " is added in database.";
            LogActions.GetLog().PrintInFile(result);
        }

        #region Properties      

        private List<vwLocation> locationList;
        public List<vwLocation> LocationList
        {
            get { return locationList; }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }
        private vwEmployee employee;
        public vwEmployee Employee
        {
            get
            { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }
        private List<tblGender> genderList;
        public List<tblGender> GenderList
        {
            get
            { return genderList; }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }        

        private List<vwManager> managers;
        public List<vwManager> Managers
        {
            get
            { return managers; }
            set
            {
                managers = value;
                OnPropertyChanged("Managers");
            }
        }

        private bool isUpdateEmployee;
        public bool IsUpdateEmployee
        {
            get { return isUpdateEmployee; }
            set { isUpdateEmployee = value; }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Save command
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        /// <summary>
        /// Method for saving data about new employee in database
        /// </summary>       
        private void SaveExecute()
        {
            try
            {
                employeeService.AddEditEmployee(Employee);
                bw.RunWorkerAsync();
                isUpdateEmployee = true;
                addEmployee.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method for check when save is possible to be Executed
        /// </summary>
        /// <returns>true or false</returns>
        private bool CanSaveExecute()
        {
            if (String.IsNullOrEmpty(Employee.FullName) || String.IsNullOrEmpty(Employee.IdentityCard) ||
                String.IsNullOrEmpty(Employee.Gender) || String.IsNullOrEmpty(Employee.PhoneNo)
                || String.IsNullOrEmpty(Employee.JMBG)||String.IsNullOrEmpty(Employee.SectorName)||String.IsNullOrEmpty(Employee.Location)||
                Employee.DateOfBirth.Equals(DateTime.Now))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Clancel command - closes the view
        /// </summary>
        private ICommand cancel;
        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }

        /// <summary>
        /// Method for closing AddIDcard View
        /// </summary>
        private void CancelExecute()
        {
            try
            {
                addEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method for check if close is possible to be Executed
        /// </summary>
        /// <returns>true </returns>
        private bool CanCancelExecute()
        {
            return true;
        }
        
        #endregion

    }
}
