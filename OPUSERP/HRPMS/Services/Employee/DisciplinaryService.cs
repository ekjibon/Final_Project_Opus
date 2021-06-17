using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class DisciplinaryService : IDisciplinaryService
    {
        private readonly ERPDbContext _context;

        public DisciplinaryService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDisciplinaryLogById(int id)
        {
            _context.disciplinaryLogs.Remove(_context.disciplinaryLogs.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DisciplinaryLog>> GetDisciplinaryLog()
        {
            return await _context.disciplinaryLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DisciplinaryLog>> GetDisciplinaryLogByEmpId(int empId)
        {
            return await _context.disciplinaryLogs.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.offense).Include(x => x.naturalPunishment).AsNoTracking().ToListAsync();
        }

        public async Task<DisciplinaryLog> GetDisciplinaryLogById(int id)
        {
            return await _context.disciplinaryLogs.FindAsync(id);
        }

        public async Task<bool> SaveDisciplinaryLog(DisciplinaryLog disciplinaryLog)
        {
            if (disciplinaryLog.Id != 0)
                _context.disciplinaryLogs.Update(disciplinaryLog);
            else
                _context.disciplinaryLogs.Add(disciplinaryLog);

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
