using Microsoft.EntityFrameworkCore;
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
    public class WagesLeaveRegisterService : IWagesLeaveRegisterService
    {
        private readonly ERPDbContext _context;

        public WagesLeaveRegisterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveLeaveRegister(WagesLeaveRegister leaveRegister)
        {
            if (leaveRegister.Id != 0)
                _context.wagesLeaveRegisters.Update(leaveRegister);
            else
                _context.wagesLeaveRegisters.Add(leaveRegister);
            await _context.SaveChangesAsync();
            return leaveRegister.Id;
        }

        public async Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegister()
        {
            return await _context.wagesLeaveRegisters.Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterByEmpIdAndStatus(int empId, int status)
        {
            return await _context.wagesLeaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == status).Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterByEmpId(int empId)
        {
            return await _context.wagesLeaveRegisters.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterByEmpIdAndDateRange(int empId, DateTime? from, DateTime? to)
        {
            return await _context.wagesLeaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == 3 && x.leaveFrom >= from && x.leaveTo <= to).Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesLeaveRegister>> GetAllLeaveRegisterPending()
        {
            return await _context.wagesLeaveRegisters.Where(x => x.leaveStatus == 1 || x.leaveStatus == 2).Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
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

        public async Task<WagesLeaveRegister> GetLeaveRegisterById(int id)
        {
            return await _context.wagesLeaveRegisters.FindAsync(id);
        }

        public async Task<int> GetLeaveBalanceByempId(int empId, int leaveType)
        {
            DateTime dateTime = DateTime.Now;
            string year = dateTime.Year.ToString();
            int half = 0;
            if (leaveType == 2)
            {
                half = await _context.wagesLeaveRegisters.Where(x => x.employeeId == empId && x.leaveStatus == 3 && x.leaveType.nameEn == "Half Day Leave").SumAsync(x => x.daysLeave);
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
            _context.wagesLeaveRegisters.Remove(_context.wagesLeaveRegisters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateLeaveApproval(int Id, int Type)
        {
            WagesLeaveRegister data = await _context.wagesLeaveRegisters.FindAsync(Id);
            if (data != null)
            {
                data.leaveStatus = Type;
                _context.wagesLeaveRegisters.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }
    }
}
