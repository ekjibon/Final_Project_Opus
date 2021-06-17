using OPUSERP.HRPMS.Data.Entity.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Travel.Interface
{
   public interface ITravelStatusLogService
    {
        Task<bool> SaveTravelStatusLog(TravelStatusLog travelStatusLog);
        Task<IEnumerable<TravelStatusLog>> GetAllTravelStatusLog();
        Task<TravelStatusLog> GetTravelStatusLogById(int id);
        Task<bool> DeleteTravelStatusLogById(int id);
        Task<IEnumerable<TravelStatusLog>> GetAllTravelStatusLogByTravelId(int id);
    }
}
