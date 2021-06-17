using OPUSERP.HRPMS.Data.Entity.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Travel.Interface
{
   public interface ITravellerInfoService
    {
        Task<bool> SaveTravellerInfo(TravellerInfo travellerInfo);
        Task<IEnumerable<TravellerInfo>> GetTravellerInfo();
        Task<TravellerInfo> GetTravellerInfoById(int id);
        Task<bool> DeleteTravellerInfoById(int id);
        Task<IEnumerable<TravellerInfo>> GetTravellerInfoByMasterId(int masterId);
    }
}
