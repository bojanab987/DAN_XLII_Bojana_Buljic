using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Services
{
    interface ILocationService
    {
        List<vwLocation> GetAllLocations();
        tblLocation AddLocation(tblLocation location);
    }
}
