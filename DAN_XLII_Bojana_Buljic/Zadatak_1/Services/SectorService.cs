using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    public class SectorService : ISectorService
    {
        /// <summary>
        /// Method for adding new sector in database
        /// </summary>
        /// <param name="sector">Sector name string</param>
        /// <returns>new sector</returns>
        public tblSector AddSector(string sector)
        {
            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {                    
                    tblSector newSector = new tblSector();
                    newSector.SectorName = sector;
                    context.tblSectors.Add(newSector);
                    context.SaveChanges();
                    return newSector;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

       
        /// <summary>
        /// Method to get all sectors from database
        /// </summary>
        /// <returns></returns>
        public List<tblSector> GetAllSectors()
        {

            try
            {
                using (EmployeeRecordsEntities context = new EmployeeRecordsEntities())
                {
                    List<tblSector> list = new List<tblSector>();
                    list = (from x in context.tblSectors select x).ToList();
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
