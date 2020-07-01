using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Services;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class EditEmployeeViewModel
    {
        EditEmployee editEmployee;
        ILocationService locationService;
        IEmployeeService employeeService;
        ISectorService sectorService;
        BackgroundWorker bw = new BackgroundWorker();

        #region Constructors
        public EditEmployeeViewModel(EditEmployee editOpen)
        {
            editEmployee = editOpen;

            //selectedLocation = new vwLocation();
            //selectedManager = new vwManager();
            //employee = new vwEmployee();

            locationService = new LocationService();
            employeeService = new EmployeeService();
            sectorService = new SectorService();

            //GenderList = employeeService.GetGenders();
            //LocationList = locationService.GetAllLocations().ToList();
            //LocationList.OrderBy(x => x.Location);
            //Managers = employeeService.GetPossibleManagers();
            //bw.DoWork += AddDoWork;

        }
        #endregion
    }
}
