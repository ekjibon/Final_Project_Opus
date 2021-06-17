using OPUSERP.Areas.HRPMSLeave.Models.NotMapped;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface IWagesLeaveRegisterService
    {
        Task<int> SaveLeaveRegister(WagesLeaveRegister leaveRegister);
        Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegister();
        Task<WagesLeaveRegister> GetLeaveRegisterById(int id);
        Task<bool> DeleteLeaveRegisterById(int id);
        Task<bool> UpdateLeaveApproval(int Id, int Type);
        Task<int> GetLeaveBalanceByempId(int empId, int leaveType);
        Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterByEmpIdAndStatus(int empId, int status);
        Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterByEmpId(int empId);
        Task<IEnumerable<LeaveRegisterNotMapped>> GetAllLeaveRegisterByEmpIdAndDate(int empId, DateTime? from, DateTime? to);
        Task<DayLeaveNotMapped> GetAllLeaveRegisterByEmpIdAndDateWithType(int empId, DateTime? from, DateTime? to, int typeId);
        Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterByEmpIdAndDateRange(int empId, DateTime? from, DateTime? to);
        Task<IEnumerable<LeaveReportModel>> GetLeaveReport(int year, int empId);
        Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterPending();
    }
}
