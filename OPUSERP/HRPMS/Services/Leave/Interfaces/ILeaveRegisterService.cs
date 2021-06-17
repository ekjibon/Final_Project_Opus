using OPUSERP.Areas.HRPMSLeave.Models;
using OPUSERP.Areas.HRPMSLeave.Models.NotMapped;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData.Interfaces
{
    public interface ILeaveRegisterService
    {
        Task<int> SaveLeaveRegister(LeaveRegister leaveRegister);
        Task<IEnumerable<LeaveRegister>> GetAllLeaveRegister();
        Task<LeaveRegister> GetLeaveRegisterById(int id);
        Task<bool> DeleteLeaveRegisterById(int id);
        Task<bool> UpdateLeaveApproval(int Id, int Type);
        Task<int> GetLeaveBalanceByempId(int empId, int leaveType);
        Task<IEnumerable<LeaveRegister>> GetAllLeaveRegisterByEmpIdAndStatus(int empId, int status);
        Task<IEnumerable<LeaveRegister>> GetAllLeaveRegisterByEmpId(int empId);
        Task<IEnumerable<LeaveRegisterNotMapped>> GetAllLeaveRegisterByEmpIdAndDate(int empId, DateTime? from, DateTime? to);
        Task<DayLeaveNotMapped> GetAllLeaveRegisterByEmpIdAndDateWithType(int empId, DateTime? from, DateTime? to, int typeId);
        Task<IEnumerable<LeaveRegister>> GetAllLeaveRegisterByEmpIdAndDateRange(int empId, DateTime? from, DateTime? to);
        Task<IEnumerable<LeaveReportModel>> GetLeaveReport(int year, int empId);
        Task<IEnumerable<LeaveSummaryReport>> GetAllLeaveSummaryReport(string year);
        Task<IEnumerable<LeaveSupervisorRecomViewModel>> GetSupervisorRecomForLeaveByEmpId(int leaveId, int empId);
        Task<DayLeaveNotMapped> GetTotalLeaveDaysBetweenDate(DateTime? frmDate, DateTime? toDate, int leaveType);
        Task<IEnumerable<LeaveBalanceViewModel>> GetLeaveBalanceSummaryByEmpId(int empId);
        Task<IEnumerable<LeaveIndividualViewModel>> GetIndividualLeaveReport(int empId, string fdate, string tdate);
    }
}
