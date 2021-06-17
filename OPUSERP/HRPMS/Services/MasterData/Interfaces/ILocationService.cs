using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ILocationService
    {
        Task<bool> SaveLocation(Location location);
        Task<IEnumerable<Location>> GetLocation();
        Task<Location> GetLocationById(int id);
       
        Task<bool> DeleteLocationById(int id);
    }
}
