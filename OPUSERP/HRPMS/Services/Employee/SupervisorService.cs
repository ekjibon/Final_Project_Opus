using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee
{
    public class SupervisorService: ISupervisorService
    {
        private readonly ERPDbContext _context;

        public SupervisorService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSupervisorById(int id)
        {
            _context.supervisors.Remove(_context.supervisors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Supervisor>> GetSupervisor()
        {
            return await _context.supervisors.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Supervisor>> GetSupervisorByEmpId(int empId)
        {
            return await _context.supervisors.Where(x => x.employeeID == empId).Include(x => x.employee).Include(x => x.supervisor).OrderBy(x => Convert.ToInt32(x.commandOrder)).AsNoTracking().ToListAsync();
        }

        public async Task<Supervisor> GetFirstSupervisorByEmpId(int empId)
        {
            return await _context.supervisors.Where(x => x.employeeID == empId).Include(x => x.employee).Include(x => x.supervisor).OrderBy(x=>Convert.ToInt32(x.commandOrder)).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Supervisor> GetSupervisorById(int id)
        {
            return await _context.supervisors.FindAsync(id);
        }

        public async Task<bool> SaveSupervisor(Supervisor supervisor)
        {
            if (supervisor.Id != 0)
                _context.supervisors.Update(supervisor);
            else
                _context.supervisors.Add(supervisor);

            return 1 == await _context.SaveChangesAsync();
        }

    }
}
