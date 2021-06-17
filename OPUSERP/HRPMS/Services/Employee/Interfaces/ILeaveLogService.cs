using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface ILeaveLogService
    {
        Task<bool> SaveLeaveLog(LeaveLog leaveLog);
        Task<IEnumerable<LeaveLog>> GetLeaveLog();
        Task<LeaveLog> GetLeaveLogById(int id);
        Task<bool> DeleteLeaveLogById(int id);
        Task<IEnumerable<LeaveLog>> GetLeaveLogByEmpId(int empId);
    }
}
