using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ILeaveStatusLogService
    {
        Task<bool> SaveLeaveStatusLog(LeaveStatusLog leaveStatusLog);
        Task<IEnumerable<LeaveStatusLog>> GetAllLeaveStatusLog();
        Task<LeaveStatusLog> GetLeaveStatusLogById(int id);
        Task<bool> DeleteLeaveStatusLogById(int id);
        Task<IEnumerable<LeaveStatusLog>> GetAllLeaveStatusLogByLeaveId(int id);
    }
}
