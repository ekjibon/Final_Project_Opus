using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSLeave.Models;
using OPUSERP.Areas.HRPMSLeave.Models.NotMapped;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave
{
    public class LeaveRegisterService : ILeaveRegisterService
    {
        private readonly ERPDbContext _context;

        public LeaveRegisterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveLeaveRegister(LeaveRegister leaveRegister)
        {
            if (leaveRegister.Id != 0)
                _context.leaveRegisters.Update(leaveRegister);
            else
                _context.leaveRegisters.Add(leaveRegister);
            await _context.SaveChangesAsync();
            return leaveRegister.Id;
        }

        public async Task<IEnumerable<LeaveRegister>> GetAllLeaveRegister()
        {
            return await _context.leaveRegisters.Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveSummaryReport>> GetAllLeaveSummaryReport(string year)
        {
            var employee = await _context.employeeInfos.AsNoTracking().ToListAsync();
            List<LeaveSummaryReport> leaveSummaryReports = new List<LeaveSummaryReport>();
            foreach (var emp in employee)
            {
                List<LeaveSummary> leaveSummaries = new List<LeaveSummary>();
                var leaveTypes = await _context.leaveTypes.AsNoTracking().ToListAsync();
                foreach (var data in leaveTypes)
                {
                    int? open = await _context.leaveOpeningBalances.Where(x => x.leaveTypeId == data.Id && x.employeeId == emp.Id && x.year.year == year).Select(x=>x.leaveDays).FirstOrDefaultAsync();
                    if (open == null)
                    {
                        open = 0;
                    }
                    int bal =await GetLeaveBalanceByempIdYear(emp.Id,data.Id,year);
                    leaveSummaries.Add(new LeaveSummary
                    {
                        type = data.nameEn,
                        Opening = (int)open,
                        Balance = bal,
                        Leave = (int)open - bal,
                    });
                }
                leaveSummaryReports.Add(new LeaveSummaryReport
                {
                    Name = emp.nameEnglish,
                    Code = emp.employeeCode,
                    leaveSummaries = leaveSummaries,
                });
            }
            return leaveSummaryReports;
        }

        public async Task<IEnumerable<LeaveRegister>> GetAllLeaveRegisterByEmpIdAndStatus(int empId, int status)
        {
            return await _context.leaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == status).Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveRegister>> GetAllLeaveRegisterByEmpId(int empId)
        {
            return await _context.leaveRegisters.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.leaveType).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveRegister>> GetAllLeaveRegisterByEmpIdAndDateRange(int empId, DateTime? from, DateTime? to)
        {
            return await _context.leaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == 3 && x.leaveFrom >= from && x.leaveTo <= to).Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveRegisterNotMapped>> GetAllLeaveRegisterByEmpIdAndDate(int empId, DateTime? from, DateTime? to)
        {
            return await _context.leaveRegisterNotMappeds.FromSql(@"LeaveApplyValidation @p0,@p1,@p2", from?.ToString("yyyyMMdd"), to?.ToString("yyyyMMdd"), empId).AsNoTracking().ToListAsync();
        }

        public async Task<DayLeaveNotMapped> GetAllLeaveRegisterByEmpIdAndDateWithType(int empId, DateTime? from, DateTime? to, int typeId)
        {
            var data = await _context.dayLeaveNotMappeds.FromSql(@"LeaveApplyValidationWithType @p0,@p1,@p2,@p3", from?.ToString("yyyyMMdd"), to?.ToString("yyyyMMdd"), empId, typeId).AsNoTracking().FirstOrDefaultAsync();
            return data;
        }

        public async Task<LeaveRegister> GetLeaveRegisterById(int id)
        {
            return await _context.leaveRegisters.Include(x => x.employee).Include(x => x.substitutionUser).Include(x => x.employee.department).Include(x => x.leaveType).AsNoTracking().Where(x => x.Id == id).FirstAsync();
        }

        public async Task<int> GetLeaveBalanceByempId(int empId, int leaveType)
        {
            DateTime dateTime = DateTime.Now;
            string year = dateTime.Year.ToString();
            int half = 0;
            if (leaveType == 2)
            {
                half = await _context.leaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == 3 && x.leaveType.nameEn == "Half Day Leave").SumAsync(x => x.daysLeave);
                half = half / 2;
            }

            var openingBalance = await _context.leaveOpeningBalances.Where(x => x.employeeId == empId && x.leaveTypeId == leaveType && x.year.year == year).FirstOrDefaultAsync();
            if (openingBalance == null)
            {
                return 0;
            }
            var leaveApprove = await _context.leaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == 3 && x.leaveTypeId == leaveType).SumAsync(x => x.daysLeave);
            int leaveBalance = (int)(openingBalance.leaveDays - leaveApprove - half);
            return leaveBalance;
        }

        public async Task<int> GetLeaveBalanceByempIdYear(int empId, int leaveType,string Year)
        {
            DateTime dateTime = DateTime.Now;
            string year = Year;
            int half = 0;
            if (leaveType == 2)
            {
                half = await _context.leaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == 3 && x.leaveType.nameEn == "Half Day Leave").SumAsync(x => x.daysLeave);
                half = half / 2;
            }

            var openingBalance = await _context.leaveOpeningBalances.Where(x => x.employeeId == empId && x.leaveTypeId == leaveType && x.year.year == year).FirstOrDefaultAsync();
            if (openingBalance == null)
            {
                return 0;
            }
            var leaveApprove = await _context.leaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == 3 && x.leaveTypeId == leaveType).SumAsync(x => x.daysLeave);
            int leaveBalance = (int)(openingBalance.leaveDays - leaveApprove - half);
            return leaveBalance;
        }

        public async Task<IEnumerable<LeaveReportModel>> GetLeaveReport(int year, int empId)
        {
            var types = await _context.leaveTypes.ToListAsync();
            List<LeaveReportModel> leaveReports = new List<LeaveReportModel>();
            int[] month = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            DateTime dateTime = new DateTime(year, 1, 1);

            var Sum = 0;

            foreach (var type in types)
            {
                var name = type.nameEn;
                var typeId = 0;
                if (type.nameEn == "Half Day Leave")
                {
                    typeId = 2;
                }
                else
                {
                    typeId = type.Id;
                }
                var openingBalance = await _context.leaveOpeningBalances.Where(x => x.employeeId == empId && x.leaveTypeId == typeId && x.year.year == year.ToString()).FirstOrDefaultAsync();

                int balance = 0;
                if (openingBalance != null)
                {
                    balance = (int)openingBalance.leaveDays;
                }
                if (balance > 0)
                {
                    for (int i = 0; i < month.Length; i++)
                    {
                        var leaveDay = await GetAllLeaveRegisterByEmpIdAndDateWithType(empId, dateTime, dateTime.AddMonths(1).AddDays(-1), type.Id);
                        month[i] = (int)leaveDay.daysLeave;
                        Sum = Sum + (int)leaveDay.daysLeave;
                        dateTime = dateTime.AddMonths(1);
                    }
                }

                var dueDay = balance - Sum;
                leaveReports.Add(new LeaveReportModel
                {
                    type = name,
                    allMonth = balance,
                    jan = month[0],
                    feb = month[1],
                    mar = month[2],
                    apr = month[3],
                    may = month[4],
                    jun = month[5],
                    jul = month[6],
                    aug = month[7],
                    sep = month[8],
                    oct = month[9],
                    nov = month[10],
                    dec = month[11],
                    total = Sum,
                    balance = dueDay
                });
                Sum = 0;
                Array.Clear(month, 0, month.Length);
                dateTime = new DateTime(year, 1, 1);
            }
            return leaveReports;
        }

        public async Task<bool> DeleteLeaveRegisterById(int id)
        {
            _context.leaveRegisters.Remove(_context.leaveRegisters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLeaveApproval(int Id, int Type)
        {
            LeaveRegister data = await _context.leaveRegisters.FindAsync(Id);
            if (data != null)
            {
                data.leaveStatus = Type;
                _context.leaveRegisters.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<IEnumerable<LeaveSupervisorRecomViewModel>> GetSupervisorRecomForLeaveByEmpId(int leaveId, int empId)
        {
            var result = await (from l in _context.leaveRegisters
                                join s in _context.supervisors on l.employeeId equals s.employeeID
                                join e in _context.employeeInfos on s.supervisorId equals e.Id
                                where l.employeeId == empId && l.Id==leaveId
                                select new LeaveSupervisorRecomViewModel
                                {
                                    supervisorName=e.nameEnglish,
                                    supervisorDesignation=e.designation,
                                    leaveStatus=l.leaveStatus,
                                    date=s.supervisorDate
                                }).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<DayLeaveNotMapped> GetTotalLeaveDaysBetweenDate(DateTime? frmDate, DateTime? toDate, int leaveType)
        {
            var data = await _context.dayLeaveNotMappeds.FromSql(@"spGetTotalLeaveDaysBetweenDate @p0,@p1,@p2", frmDate?.ToString("yyyyMMdd"), toDate?.ToString("yyyyMMdd"), leaveType).AsNoTracking().FirstOrDefaultAsync();
            return data;            
        }

        public async Task<IEnumerable<LeaveBalanceViewModel>> GetLeaveBalanceSummaryByEmpId(int empId)
        {
            return await _context.leaveBalanceViewModels.FromSql(@"spLoadIndividualLeaveSummary @p0", empId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveIndividualViewModel>> GetIndividualLeaveReport(int empId, string fdate, string tdate)
        {
            return await _context.leaveIndividualViewModels.FromSql($"spGetLeaveDetailsNew {empId},{Convert.ToDateTime(fdate).ToString("yyyyMMdd")},{Convert.ToDateTime(tdate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }
    }
}
