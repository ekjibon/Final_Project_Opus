using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.HRPMS.Data.Entity.Leave;

namespace OPUSERP.HRPMS.Services.Leave.Interfaces
{
    public interface ILeaveTypeService
    {
        Task<bool> SaveLeaveType(LeaveType leaveType);
        Task<IEnumerable<LeaveType>> GetAllLeaveType();
        Task<IEnumerable<LeaveType>> GetAllLeaveTypeBySP(int? empId);
        Task<LeaveType> GetLeaveTypeById(int id);
        Task<bool> DeleteLeaveTypeById(int id);
    }
}
