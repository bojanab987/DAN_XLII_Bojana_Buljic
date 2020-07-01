using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    interface ISectorService
    {
        tblSector AddSector(tblSector sector);
        List<tblSector> GetAllSectors();
        tblSector GetSector(string sector);
    }
}
