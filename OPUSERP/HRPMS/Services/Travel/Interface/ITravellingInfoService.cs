using OPUSERP.HRPMS.Data.Entity.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Travel.Interface
{
   public interface ITravellingInfoService
    {
        Task<bool> SaveTravellingInfo(TravellingInfo travellingInfo);
        Task<IEnumerable<TravellingInfo>> GetTravellingInfo();
        Task<TravellingInfo> GetTravellingInfoById(int id);
        Task<bool> DeleteTravellingInfoById(int id);
        Task<IEnumerable<TravellingInfo>> GetTravellingInfoByMasterId(int masterId);
    }
}
