using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class AddEmployeeViewModel:ViewModelBase
    {
        AddEmployee addEmployee;
        ILocationService locationService;
        IEmployeeService employeeService;
        ISectorService sectorService;

        #region Constructors
        public AddEmployeeViewModel(AddEmployee addEmployeeOpen)
        {
            addEmployee = addEmployeeOpen;

            selectedLocation = new vwLocation();
            selectedManager = new vwManager();
            employee = new vwEmployee();

            locationService = new LocationService();
            employeeService = new EmployeeService();
            sectorService = new SectorService();

            LocationList = locationService.GetAllLocations().ToList();
            LocationList.OrderBy(x => x.Location);

            //vwManager aManager = employeeService.GetManager(" ");
            //if(aManager==null)
            //{

            //}
            Managers = employeeService.GetPossibleManagers();
        }
        #endregion

        #region Properties
        private vwLocation selectedLocation;
        public vwLocation SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                selectedLocation = value;
                OnPropertyChanged("SelectedLocation");
            }
        }

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
            {return employee;}
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private vwManager selectedManager;
        public vwManager SelectedManager
        {
            get
            { return selectedManager;}
            set
            {
                selectedManager = value;
                OnPropertyChanged("SelectedManager");
            }
        }

        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        private string sector;
        public string Sector
        {
            get
            {return sector;}
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private string gender="male";
        public string Gender
        {
            get
            {return gender;}
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
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


    }
}
