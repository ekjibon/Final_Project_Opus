using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Attendance.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Attendance
{
    public class AttendanceProcessService : IAttendanceProcessService
    {
        private readonly ERPDbContext _context;
        public AttendanceProcessService(ERPDbContext context)
        {
            _context = context;
        }

        #region Emp Attendance

        public async Task<bool> SaveEmpAttendence(EmpAttendance attendence)
        {
            if (attendence.Id != 0)
                _context.empAttendances.Update(attendence);
            else
                _context.empAttendances.Add(attendence);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateEmpAttendence(int id, EmpAttendance attendence)
        {
            _context.empAttendances.Update(attendence);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmpAttendanceViewModel>> ProcessAttendance(DateTime startDate, int? SbuID)
        {
            return await _context.ProcessAttendanceSP.FromSql($"Dailyattendence_New {startDate},{SbuID}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmpAttendanceViewModel>> ProcessAttendanceFromMachine(DateTime startDate, int? SbuID)
        {
            return await _context.ProcessAttendanceSP.FromSql($"Dailyattendence_New_Machine {startDate},{SbuID}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmpAttendanceViewModel>> ProcessAttendanceFromExcel(DateTime startDate, int? SbuID)
        {
            return await _context.ProcessAttendanceSP.FromSql($"Dailyattendence_New_Excel {startDate},{SbuID}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmpAttendanceViewModel>> WagesProcessAttendance(string startDate, int? SbuID)
        {
            return await _context.ProcessAttendanceSP.FromSql($"Dailyattendence_New_Wages {startDate},{SbuID}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region Attendence Api

        public async Task<bool> SaveAttendence(AttendenceApi attendence)
        {
            if (attendence.Id != 0)
                _context.AttendenceApi.Update(attendence);
            else
                _context.AttendenceApi.Add(attendence);
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateAttendence(int id, AttendenceApi attendence)
        {

            _context.AttendenceApi.Update(attendence);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AttendenceApi>> GetAllAttendence(string empcode)
        {
            return await _context.AttendenceApi.Where(x => x.empCode == empcode).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AttendanceViewModelApi>> GetAllAttendenceByEmpIdAndDateRange(string empId, string from, string to)
        {
            //return await _context.AttendanceViewModelApis.FromSql($"IndividualAttendanceReport_Api {empId},{Convert.ToDateTime(from).ToString("yyyyMMdd")},{Convert.ToDateTime(to).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
            return await _context.AttendanceViewModelApis.FromSql($"IndividualAttendanceReport_Api {empId},{from},{to}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AttendenceApi>> GetAttendenceApi()
        {
            return await _context.attendenceApis.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteattendenceApis()
        {
            _context.attendenceApis.RemoveRange(_context.attendenceApis.ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveAttendenceApi(AttendenceApi employeePunchCardInfo)
        {
            if (employeePunchCardInfo.Id != 0)
                _context.attendenceApis.Update(employeePunchCardInfo);
            else
                _context.attendenceApis.Add(employeePunchCardInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Upload Attendance Excel

        public async Task<bool> SaveAttendenceUpload(AttendenceUpload attendenceUpload)
        {
            if (attendenceUpload.Id != 0)
                _context.attendenceUploads.Update(attendenceUpload);
            else
                _context.attendenceUploads.Add(attendenceUpload);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AttendenceUpload>> GetAllAttendenceUpload()
        {
            return await _context.attendenceUploads.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteAttendenceUpload()
        {
            _context.attendenceUploads.RemoveRange(_context.attendenceUploads.ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Attendance Apply

        public async Task<int> SaveAttendanceUpdateApply(AttendanceUpdateApply attendanceUpdateApply)
        {
            if (attendanceUpdateApply.Id != 0)
                _context.attendanceUpdateApplies.Update(attendanceUpdateApply);
            else
                _context.attendanceUpdateApplies.Add(attendanceUpdateApply);
            await _context.SaveChangesAsync();
            return attendanceUpdateApply.Id;
        }

        public async Task<IEnumerable<AttendanceUpdateApply>> GetAllAttendanceUpdateApply()
        {
            return await _context.attendanceUpdateApplies.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AttendanceUpdateApply>> GetAttendanceUpdateApplyByEmpId(int? empId)
        {
            return await _context.attendanceUpdateApplies.Include(x => x.employeeInfo).Where(a => a.employeeInfoId == empId).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAttendanceUpdateApply(int Id, int status)
        {
            AttendanceUpdateApply data = await _context.attendanceUpdateApplies.FindAsync(Id);
            if (data != null)
            {
                data.approvalStatus = status;
                _context.attendanceUpdateApplies.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<bool> DeleteAttendanceUpdateApplyById(int id)
        {
            _context.attendanceUpdateApplies.Remove(_context.attendanceUpdateApplies.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Loan Route

        public async Task<bool> SaveAttendanceRoute(AttendanceRoute attendanceRoute)
        {
            if (attendanceRoute.Id != 0)
                _context.attendanceRoutes.Update(attendanceRoute);
            else
                _context.attendanceRoutes.Add(attendanceRoute);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AttendanceRoute>> GetAttendanceRouteByEmpId(int empId)
        {
            return await _context.attendanceRoutes.Include(x => x.attendanceUpdateApply.employeeInfo.department).Where(x => x.employeeId == empId && x.isActive == 1).AsNoTracking().ToListAsync();
        }

        public async Task<AttendanceRoute> GetAttendanceRouteById(int id)
        {
            return await _context.attendanceRoutes.FindAsync(id);
        }

        public async Task<bool> UpdateAttendanceRoute(int Id, int Type)
        {
            AttendanceRoute data = await _context.attendanceRoutes.FindAsync(Id);
            if (data != null)
            {
                data.isActive = Type;
                _context.attendanceRoutes.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<AttendanceRoute> GetAttendanceRouteByRouteOrder(int id, int order)
        {
            return await _context.attendanceRoutes.Where(x => x.attendanceUpdateApplyId == id && x.routeOrder == order).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAttendanceRouteByMasterId(int masterId)
        {
            _context.attendanceRoutes.RemoveRange(_context.attendanceRoutes.Where(a => a.attendanceUpdateApplyId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Attendence Report

        public async Task<IEnumerable<JobCardReportViewModel>> GetDailyJobCardReport(int? DeptId, string fdate, string tdate, int? ShiftId, int? EmpId, int? summaryId)
        {
            return await _context.JobCardReportViewModels.FromSql($"EmployeeReportDaily {DeptId},{fdate},{tdate},{ShiftId},{EmpId},{summaryId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<JobCardReportViewModel>> GetDailyJobCardWagesReport(string DeptName, string fdate, string tdate, int? SbuID, int? BranchID, int? summaryID)
        {
            return await _context.JobCardReportViewModels.FromSql($"WagesReportDaily {DeptName},{fdate},{tdate},{SbuID},{BranchID},{summaryID}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IndividualAttendanceReportViewModel>> GetIndividualAttendanceReport(int EmpID, string fdate, string tdate)
        {
            return await _context.individualAttendanceReportViewModels.FromSql($"IndividualAttendanceReport {EmpID},{fdate},{tdate}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IndividualAttendanceReportViewModel>> GetWagesIndividualAttendanceReport(int EmpID, string fdate, string tdate)
        {
            return await _context.individualAttendanceReportViewModels.FromSql($"WagesIndividualAttendanceReport {EmpID},{fdate},{tdate}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IndividualAttendanceSummaryReportViewModel>> GetIndividualAttendanceSummaryReport(int EmpID, string fdate, string tdate, int? DeptId, int? ShiftId)
        {
            return await _context.individualAttendanceSummaryReportViewModels.FromSql($"spAttendanceSummary {EmpID},{fdate},{tdate},{DeptId},{ShiftId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IndividualAttendanceSummaryReportViewModel>> GetWagesIndividualAttendanceSummaryReport(int EmpID, string fdate, string tdate, int? SbuID, int? BranchID)
        {
            return await _context.individualAttendanceSummaryReportViewModels.FromSql($"spWagesAttendanceSummary {EmpID},{fdate},{tdate},{SbuID},{BranchID}").AsNoTracking().ToListAsync();
        }


        #endregion
    }
}