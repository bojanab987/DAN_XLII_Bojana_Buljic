using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    public class LocationService : ILocationService
    {
        /// <summary>
        /// Method for adding location into database
        /// </summary>
        /// <param name="location"></param>
        /// <returns>new location</returns>
        public tblLocation AddLocation(tblLocation location)
        {
            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {
                    tblLocation newLocation = new tblLocation();
                    newLocation.Street = location.Street;
                    newLocation.City = location.City;
                    newLocation.Country = location.Country;
                    context.tblLocations.Add(newLocation);
                    context.SaveChanges();
                    location.LocationID = newLocation.LocationID;

                    //file loging background worker
                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogAddLocationToFile(location);

                    return location;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method gets all locations from database
        /// </summary>
        /// <returns>list of locations</returns>
        public List<vwLocation> GetAllLocations()
        {
            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {
                    List<vwLocation> list = new List<vwLocation>();
                    list = (from x in context.vwLocations select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
