using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Leave
{
    public class LeaveStatusLogService : ILeaveStatusLogService
    {
        private readonly ERPDbContext _context;

        public LeaveStatusLogService(ERPDbContext context)
        {
            _context = context;
        }

        public async  Task<bool> SaveLeaveStatusLog(LeaveStatusLog leaveStatusLog)
        {
            if (leaveStatusLog.Id != 0)
                _context.leaveStatusLogs.Update(leaveStatusLog);
            else
                _context.leaveStatusLogs.Add(leaveStatusLog);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveStatusLog>> GetAllLeaveStatusLog()
        {
            return await _context.leaveStatusLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LeaveStatusLog>> GetAllLeaveStatusLogByLeaveId(int id)
        {
            return await _context.leaveStatusLogs.Where(x=>x.leaveRegisterId==id).Include(x=>x.leaveRegister.leaveType).Include(x=>x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<LeaveStatusLog> GetLeaveStatusLogById(int id)
        {
            return await _context.leaveStatusLogs.FindAsync(id);
        }

        public async  Task<bool> DeleteLeaveStatusLogById(int id)
        {
            _context.leaveStatusLogs.Remove(_context.leaveStatusLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        
    }
}
