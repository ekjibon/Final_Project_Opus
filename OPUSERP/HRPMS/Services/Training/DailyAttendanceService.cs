using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training
{
    public class DailyAttendanceService : IDailyAttendanceService
    {
        private readonly ERPDbContext _context;

        public DailyAttendanceService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteDailyAttendanceId(int id)
        {
            _context.sessionAttendances.Remove(_context.sessionAttendances.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SessionAttendance>> GetAllDailyAttendance()
        {
            return await _context.sessionAttendances.AsNoTracking().ToListAsync();
        }

        public async Task<SessionAttendance> GetDailyAttendanceId(int id)
        {
            return await _context.sessionAttendances.FindAsync(id);
        }

        public async Task<bool> SaveDailyAttendance(SessionAttendance dailyAttendance)
        {
            if (dailyAttendance.Id != 0)
                _context.sessionAttendances.Update(dailyAttendance);
            else
                _context.sessionAttendances.Add(dailyAttendance);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
