using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave
{
    public class LeavePolicyService : ILeavePolicyService
    {
        private readonly ERPDbContext _context;

        public LeavePolicyService(ERPDbContext context)
        {
            _context = context;
        }

        #region Leave Policy

        public async Task<bool> SaveLeavePolicy(LeavePolicy leavePolicy)
        {
            if (leavePolicy.Id != 0)
                _context.leavePolicies.Update(leavePolicy);
            else
                _context.leavePolicies.Add(leavePolicy);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeavePolicy>> GetLeavePolicy()
        {
            return await _context.leavePolicies.Include(x => x.leaveType).Include(x => x.year).AsNoTracking().ToListAsync();
        }

        public async Task<LeavePolicy> GetLeavePolicyById(int id)
        {

            return await _context.leavePolicies.FindAsync(id);
        }

        public async Task<LeavePolicy> GetLeavePolicyByTypeandYear(int typeId, int year)
        {
            return await _context.leavePolicies.Where(x => x.leaveTypeId == typeId && x.yearId == year).Include(x => x.leaveType).Include(x => x.year).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteLeavePolicyById(int id)
        {
            _context.leavePolicies.Remove(_context.leavePolicies.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Leave Opening Balance

        public async Task<bool> SaveLeaveOpeningBalance(LeaveOpeningBalance leaveOpeningBalance)
        {
            if (leaveOpeningBalance.Id != 0)
                _context.leaveOpeningBalances.Update(leaveOpeningBalance);
            else
                _context.leaveOpeningBalances.Add(leaveOpeningBalance);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveOpeningBalance>> GetLeaveOpeningBalance()
        {
            return await _context.leaveOpeningBalances.Include(x => x.employee).Include(x => x.leaveType).Include(x => x.year).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveOpeningBalance>> GetLeaveOpeningBalanceByEmpAndYear(int empId, int year)
        {
            return await _context.leaveOpeningBalances.Where(x => x.employeeId == empId && x.yearId == year).Include(x => x.employee).Include(x => x.leaveType).Include(x => x.year).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteLeaveOpeningBalanceById(int id)
        {
            _context.leaveOpeningBalances.Remove(_context.leaveOpeningBalances.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> OpeningBalanceProcess(int id)
        {
            LeavePolicy leavePolicy = await _context.leavePolicies.FindAsync(id);
            IEnumerable<EmployeeInfo> employeeInfos = await _context.employeeInfos.ToListAsync();

            foreach (var data in employeeInfos)
            {
                LeaveOpeningBalance leaveOpeningBalance = new LeaveOpeningBalance
                {
                    yearId = leavePolicy.yearId,
                    leaveTypeId = leavePolicy.leaveTypeId,
                    employeeId = data.Id,
                    leaveDays = leavePolicy.yearlyMaxLeave,
                    leaveCarryDays = leavePolicy.yearlyMaxCarry
                };
                _context.leaveOpeningBalances.Add(leaveOpeningBalance);
                await _context.SaveChangesAsync();
            }

            return false;
        }

        #endregion

        #region Leave Day
        public async Task<bool> SaveLeaveDay(LeaveDay leaveDay)
        {
            if (leaveDay.Id != 0)
                _context.leaveDays.Update(leaveDay);
            else
                _context.leaveDays.Add(leaveDay);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveDay>> GetAllLeaveDay()
        {
            return await _context.leaveDays.AsNoTracking().ToListAsync();
        }

        public async Task<LeaveDay> GetLeaveDayById(int id)
        {

            return await _context.leaveDays.FindAsync(id);
        }

        public async Task<bool> DeleteLeaveDayById(int id)
        {
            _context.leaveDays.Remove(_context.leaveDays.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        

        #endregion
    }
}
