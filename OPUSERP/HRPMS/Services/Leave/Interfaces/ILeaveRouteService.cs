using OPUSERP.HRPMS.Data.Entity.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave.Interfaces
{
   public interface ILeaveRouteService
    {
        Task<bool> SaveLeaveRoute(LeaveRoute leaveRoute);
        Task<IEnumerable<LeaveRoute>> GetLeaveRoute();
        Task<LeaveRoute> GetLeaveRouteById(int id);
        Task<bool> DeleteLeaveRouteById(int id);
        Task<IEnumerable<LeaveRoute>> GetLeaveRouteByEmpId(int empId);
        Task<bool> UpdateLeaveRoute(int Id, int Type);
        Task<LeaveRoute> GetLeaveRouteByRouteOrder(int id, int order);
        Task<LeaveRoute> GetLeaveRouteByLeaveRegisterId(int leaveId);
    }
}
