using OPUSERP.HRPMS.Data.Entity.Travel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Travel.Interface
{
   public interface ITravelRouteService
    {
        Task<bool> SaveTravelRoute(TravelRoute travelRoute);
        Task<IEnumerable<TravelRoute>> GetTravelRoute();
        Task<TravelRoute> GetTravelRouteById(int id);
        Task<bool> DeleteTravelRouteById(int id);
        Task<IEnumerable<TravelRoute>> GetTravelRouteByEmpId(int empId);
        Task<bool> UpdateTravelRoute(int Id, int Type);
        Task<TravelRoute> GetTravelRouteByRouteOrder(int id, int order);
        Task<TravelRoute> GetTravelRouteByTravelMasterId(int leaveId);
    }
}
