using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class LeaveLogService : ILeaveLogService
    {
        private readonly ERPDbContext _context;

        public LeaveLogService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteLeaveLogById(int id)
        {
            _context.leaveLogs.Remove(_context.leaveLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveLog>> GetLeaveLog()
        {
            return await _context.leaveLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveLog>> GetLeaveLogByEmpId(int empId)
        {
            return await _context.leaveLogs.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.leaveType).AsNoTracking().ToListAsync();
        }

        public async Task<LeaveLog> GetLeaveLogById(int id)
        {
            return await _context.leaveLogs.FindAsync(id);
        }

        public async Task<bool> SaveLeaveLog(LeaveLog leaveLog)
        {
            if (leaveLog.Id != 0)
                _context.leaveLogs.Update(leaveLog);
            else
                _context.leaveLogs.Add(leaveLog);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
