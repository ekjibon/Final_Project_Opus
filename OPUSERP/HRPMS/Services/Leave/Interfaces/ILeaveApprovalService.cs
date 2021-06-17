using OPUSERP.HRPMS.Data.Entity.Leave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave.Interfaces
{
    public interface ILeaveApprovalService
    {
        // JobCircular
        Task<int> SaveLeaveApproval(LeaveRegister leaveRegister);
        Task<IEnumerable<LeaveRegister>> GetAllLeaveApproval(string status);
        Task<LeaveRegister> GetLeaveApprovalById(int id);
        Task<bool> DeleteLeaveApprovalById(int id);
        Task<bool> UpdateLeaveApproval(int Id, string Type);
    }
}
