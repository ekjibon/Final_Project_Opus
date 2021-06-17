using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Attendance.Interfaces
{
    public interface IAttendanceProcessService
    {
        #region Emp Attendance

        Task<bool> SaveEmpAttendence(EmpAttendance attendence);
        Task<bool> UpdateEmpAttendence(int id, EmpAttendance attendence);
        Task<IEnumerable<EmpAttendanceViewModel>> ProcessAttendance(DateTime startDate, int? SbuID);
        Task<IEnumerable<EmpAttendanceViewModel>> ProcessAttendanceFromMachine(DateTime startDate, int? SbuID);
        Task<IEnumerable<EmpAttendanceViewModel>> ProcessAttendanceFromExcel(DateTime startDate, int? SbuID);
        Task<IEnumerable<EmpAttendanceViewModel>> WagesProcessAttendance(string startDate, int? SbuID);

        #endregion

        #region Attendence Api

        Task<bool> SaveAttendence(AttendenceApi attendence);
        Task<bool> UpdateAttendence(int id, AttendenceApi attendence);
        Task<IEnumerable<AttendenceApi>> GetAllAttendence(string empcode);
        Task<IEnumerable<AttendenceApi>> GetAttendenceApi();
        Task<bool> DeleteattendenceApis();
        Task<bool> SaveAttendenceApi(AttendenceApi employeePunchCardInfo);
        Task<IEnumerable<AttendanceViewModelApi>> GetAllAttendenceByEmpIdAndDateRange(string empId, string from, string to);

        #endregion

        #region Upload Attendance Excel

        Task<bool> SaveAttendenceUpload(AttendenceUpload attendenceUpload);
        Task<IEnumerable<AttendenceUpload>> GetAllAttendenceUpload();
        Task<bool> DeleteAttendenceUpload();

        #endregion

        #region Attendance Apply

        Task<int> SaveAttendanceUpdateApply(AttendanceUpdateApply attendanceUpdateApply);
        Task<IEnumerable<AttendanceUpdateApply>> GetAllAttendanceUpdateApply();
        Task<IEnumerable<AttendanceUpdateApply>> GetAttendanceUpdateApplyByEmpId(int? empId);
        Task<bool> UpdateAttendanceUpdateApply(int Id, int status);
        Task<bool> DeleteAttendanceUpdateApplyById(int id);
        #endregion

        #region Loan Route

        Task<bool> SaveAttendanceRoute(AttendanceRoute attendanceRoute);
        Task<IEnumerable<AttendanceRoute>> GetAttendanceRouteByEmpId(int empId);
        Task<AttendanceRoute> GetAttendanceRouteById(int id);
        Task<bool> UpdateAttendanceRoute(int Id, int Type);
        Task<AttendanceRoute> GetAttendanceRouteByRouteOrder(int id, int order);
        Task<bool> DeleteAttendanceRouteByMasterId(int masterId);
        #endregion

        #region Attendence Report

        Task<IEnumerable<JobCardReportViewModel>> GetDailyJobCardReport(int? DeptId, string fdate, string tdate, int? ShiftId, int? EmpId, int? summaryId);
        Task<IEnumerable<IndividualAttendanceReportViewModel>> GetIndividualAttendanceReport(int EmpID, string fdate, string tdate);
        Task<IEnumerable<IndividualAttendanceSummaryReportViewModel>> GetIndividualAttendanceSummaryReport(int EmpID, string fdate, string tdate, int? DeptId, int? ShiftId);
        Task<IEnumerable<JobCardReportViewModel>> GetDailyJobCardWagesReport(string DeptName, string fdate, string tdate, int? SbuID, int? BranchID, int? summaryID);
        Task<IEnumerable<IndividualAttendanceReportViewModel>> GetWagesIndividualAttendanceReport(int EmpID, string fdate, string tdate);
        Task<IEnumerable<IndividualAttendanceSummaryReportViewModel>> GetWagesIndividualAttendanceSummaryReport(int EmpID, string fdate, string tdate, int? SbuID, int? BranchID);

        #endregion

    }
}
