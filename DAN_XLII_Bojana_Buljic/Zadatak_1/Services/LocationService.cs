using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    public class LocationService : ILocationService
    {
        static string file = @"../../Files/Locations.txt";
        static EmployeeRecordsEntities context = new EmployeeRecordsEntities();

        /// <summary>
        /// Method for reading locations from file
        /// </summary>
        /// <returns>List of locations</returns>
        public List<string> ReadLocationsFile()
        {
            List<string> locations = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        locations.Add(line);
                    }
                }
                return locations;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not possible to read file");
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for adding locations from file into database
        /// </summary>        
        public void AddLocation()
        {                        
            tblLocation location = new tblLocation();
            List<string> locations = ReadLocationsFile();
            foreach (var loc in locations)
            {
                string[] lines = loc.Split(',');
                location.Street = lines[0];
                location.City = lines[1];
                location.Country = lines[2];

                context.tblLocations.Add(location);
                context.SaveChanges();
            }           

                    //file loging background worker
                    //FileLoging fileLog = FileLoging.Instance();
                    //fileLog.LogAddLocationToFile(location);                 
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
