using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
   public interface ITravelVehicleTypeService
    {
        Task<bool> SaveTravelVehicleType(TravelVehicleType travelVehicleType);
        Task<IEnumerable<TravelVehicleType>> GetTravelVehicleType();
        Task<TravelVehicleType> GetTravelVehicleTypeById(int id);
        Task<bool> DeleteTravelVehicleTypeById(int id);
    }
}
